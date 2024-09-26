using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using static txtrpg.Program;


namespace txtrpg
{
    internal class Program
    {
        // 플레이어
        static Player player = new Player("", 1, "전사", 10, 5, 100, 3000);

        // 아이템
        static List<Item> items = new List<Item>
        {
            new Item("수련자 갑옷", "방어력 +5", 0, 5, 1000),
            new Item("무쇠 갑옷", "방어력 +9", 0, 9, 2000),
            new Item("스파르타의 갑옷", "방어력 +15", 0, 15, 3500),
            new Item("낡은 검", "공격력 +2", 2, 0, 600),
            new Item("청동 도끼", "공격력 +5", 5, 0, 1500),
            new Item("스파르타의 창", "공격력 +7", 7, 0, 2500),
            new Item("나무 방패", "방어력 +5", 0, 5 , 900),
            new Item("타워형 철 방패", "방어력 +10", 0, 10, 2100),
            new Item("스파르타의 방패", "방어력 +15", 0, 15, 3000)
        };
        // 플레이어가 소지한 아이템
        static List<Item> playeritems = new List<Item> { };

        static void Main()
        {
            //시작화면
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.Write(">>");
            // 이름 입력
            player.name = Console.ReadLine();
            Console.Write("\n이름 : " + player.name + "\n");

            Menu();
        }

        public static void Menu()
        {
            Console.Clear();
            // 메인 화면
            Console.WriteLine("이곳에서 던전 입장전에 활동을 할 수 있습니다. \n");

            Console.WriteLine("1. 상태 보기"); 
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점"); 
            Console.WriteLine("4. 휴식\n"); 

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            // 화면 선택
            while (true)
            {
                int select = 0;
                if (int.TryParse(Console.ReadLine(), out select))
                {
                    if (select == 1) // 상태 보기
                    {
                        Status();
                        break;
                    }
                    else if (select == 2) // 인벤토리
                    {
                        Inventory();
                        break;
                    }
                    else if (select == 3) // 상점
                    {
                        Shop();
                        break;
                    }
                    else if (select == 4) // 휴식
                    {
                        Rest();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                }
            }
        }

        public static void Inventory()
        {
            Console.Clear();
            Console.WriteLine("[아이템 목록]"); // 아이템 목록
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Purchase == true)
                {
                    if (items[i].Attack > 0 && items[i].Defense == 0)
                    {
                        if (player.equipitematk == true)
                        {
                            Console.WriteLine($"[E] - {items[i].ItemName}    |   공격력 +{items[i].Attack}"); // 장착했다면 [E] 표시
                        }
                        else
                        {
                            Console.WriteLine($"- {items[i].ItemName}    |   공격력 +{items[i].Attack}");
                        }
                    }
                    else if(items[i].Attack == 0 && items[i].Defense > 0)
                    {
                        if (player.equipitemdef == true)
                        {
                            Console.WriteLine($"[E] - {items[i].ItemName}    |   방어력 +{items[i].Defense}"); // 장착했다면 [E] 표시
                        }
                        else
                        { 
                            Console.WriteLine($"- {items[i].ItemName}    |   방어력 +{items[i].Defense}"); 
                        }
                    }
                }
            }

            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            while (true)
            {
                int select;
                if (int.TryParse(Console.ReadLine(), out select))
                {
                    if (select == 1) // 장착창 켜기
                    {
                        Equip();
                        break;
                    }
                    else if (select == 0) // 나가기
                    {
                        Menu();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                }
            }
        }

        public static void Equip()
        {
            int counter = 1;
            Console.Clear();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Purchase == true)
                {
                    playeritems.Add(items[i]);
                    if (items[i].Attack > 0 && items[i].Defense == 0)
                    {
                        if (player.equipitematk == true)
                        {
                            Console.WriteLine($"{counter}. [E] {items[i].ItemName}    |   공격력 +{items[i].Attack}"); 
                            counter++;
                        }
                        else
                        {
                            Console.WriteLine($"{counter}. {items[i].ItemName}    |   공격력 +{items[i].Attack}");
                            counter++;
                        }
                    }
                    else if (items[i].Attack == 0 && items[i].Defense > 0)
                    {
                        if (player.equipitemdef == true)
                        {
                            Console.WriteLine($"{counter}. [E] {items[i].ItemName}    |   방어력 +{items[i].Defense}");
                            counter++;
                        }
                        else
                        {
                            Console.WriteLine($"{counter}. {items[i].ItemName}    |   방어력 +{items[i].Defense}");
                            counter++;
                        }
                    }
                }
            }
            Console.WriteLine("\n장착하거나 해제하실 번호를 입력해주세요.");
            Console.WriteLine("0. 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            while (true)
            {
                int select;
                if (int.TryParse(Console.ReadLine(), out select) && select <= counter)
                {
                    if (select == 0) // 인벤토리창으로 
                    {
                        Inventory();
                        break;
                    }
                    else // 장착 , 해제완료
                    {
                        if (select > 0 && select < counter)
                        {
                            if (playeritems[select - 1].Attack > 0 && playeritems[select - 1].Defense == 0) // 장착 아이템이 무기일때
                            {
                                if (player.equipitematk == false)
                                {
                                    player.equipitematk = true;
                                    player.atk += playeritems[select - 1].Attack; // 장착 아이템의 공격력만큼 플레이어 공격력 증가
                                    Console.WriteLine("장착을 완료했습니다.");
                                }
                                else if (player.equipitematk == true)
                                {
                                    player.equipitematk = false;
                                    Console.WriteLine("해제를 완료했습니다.");
                                }
                            }
                            else if (playeritems[select - 1].Defense > 0 && playeritems[select - 1].Attack== 0) // 장착 아이템이 방어구일때
                            {
                                if (player.equipitemdef == false) 
                                {
                                    player.equipitemdef = true;
                                    player.def += playeritems[select - 1].Defense; // 장착 아이템의 방어력만큼 플레이어 공격력 증가
                                    Console.WriteLine("장착을 완료했습니다.");
                                }
                                else if (player.equipitemdef == true)
                                {
                                    player.equipitemdef = false;
                                    Console.WriteLine("해제를 완료했습니다.");
                                }
                            }
                        }
                    }
                }
                else
                {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                continue;
                }
                Console.WriteLine("\n장착하거나 해제하실 번호를 입력해주세요.");
                Console.WriteLine("0. 나가기");
                Console.Write(">>");
            }
        }


        public static void Shop()
        {
            Console.Clear();
            Console.WriteLine("상점에 오신걸 환영합니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine(player.gold + "G\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                // 아이템 이름, 설명, 가격, 구매완료 여부
                Console.WriteLine($"{items[i].ItemName} | {items[i].ToolTip} | {items[i].Price}G {(items[i].Purchase ? " - 구매 완료" : "")}");
            }
            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            while (true)
            {
                int select = 0;
                if (int.TryParse(Console.ReadLine(), out select))
                {
                    if (select == 0)
                    {
                        Menu(); // 상점 나가기
                    }
                    else if (select == 1)
                    {
                        BuyItem(items); // 아이템 구매창으로 가기
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                }
            }
        }

        static void BuyItem(List<Item> items)
        {
            void BuyItemCamera()
            { 
                 Console.Clear();
                 Console.WriteLine("상점에 오신걸 환영합니다.\n");
                 Console.WriteLine("[보유 골드]");
                 Console.WriteLine(player.gold + "G\n");

                 Console.WriteLine("[아이템 목록]");
                 for (int i = 0; i < items.Count; i++)
                 {
                     Console.WriteLine($"{i + 1}. {items[i].ItemName} | {items[i].ToolTip} | {items[i].Price}G {(items[i].Purchase ? " - 구매 완료" : "")}");
                 }
                 Console.WriteLine("\n구매하실 아이템을 선택해주세요.");
                 Console.Write(">>");
            }

            BuyItemCamera();
            int select;
            if (int.TryParse(Console.ReadLine(), out select) && select >= 0 && select <= items.Count)
            {
                if (select == 0)
                {
                    Shop(); // 상점창으로
                }
                else // 구매시도
                {
                    Item buyItems = items[select - 1];
                    if (buyItems.Purchase) // 이미 구매가 된 아이템
                    {
                        Console.WriteLine("이미 구매한 아이템입니다."); 
                    }
                    else if (player.gold >= buyItems.Price) // 플레이어의 소지 골드가 상점가격보다 높을경우
                    {
                        Console.WriteLine("구매를 완료했습니다.");
                        player.gold -= buyItems.Price;
                        buyItems.Purchase = true;
                    }
                    else // 플레이어의 소지 골드가 상점가격보다 낮을경우
                    {
                        Console.WriteLine("골드가 부족합니다");
                    }
                }
                Console.WriteLine("다시 입력해주세요.");
                Console.WriteLine("0. 상점나가기 1. 구매창으로");
                Console.WriteLine(">>");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                return;
            }
        }
        public static void Status() // 상태창
        {
            Console.Clear();
            Console.WriteLine("현재 캐릭터의 정보입니다.\n");
            Console.WriteLine("이름: "+ player.name);
            Console.WriteLine("Lv. " + player.level);
            Console.WriteLine("Job : " + player.job);
            Console.WriteLine("공격력 : " + player.atk);
            Console.WriteLine("방어력 : " + player.def);
            Console.WriteLine("체력 : " + player.hp);
            Console.WriteLine("Gold : " + player.gold + "G\n");

            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            while (true)
            {
                int select = 0;
                if (int.TryParse(Console.ReadLine(), out select))
                {
                    if (select == 0)
                    {
                        Menu(); // 메인 화면으로 이동
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                }
            }
        }

        public static void Rest()
        {
            Console.Clear();
            Console.WriteLine("[여관에서 휴식]\n");
            Console.WriteLine("500G을 내면 체력을 회복할 수 있습니다.  (보유 골드 : " + player.gold + " G)\n");

            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            while (true)
            {
                int select = 0;
                if (int.TryParse(Console.ReadLine(), out select))
                {
                    if (select == 1)
                    {
                        if (player.gold > 500)
                        {
                            if (player.hp == 100)
                            {
                                Console.WriteLine($"이미 {player.name}의 상태는 완벽합니다");
                            }
                            else
                            {
                                player.hp = 100;
                                Console.WriteLine("여관에서 휴식을 완료했습니다.     ( 소지 골드 -500G ) ");
                                player.gold -= 500;
                            }
                        }
                        else
                        {
                            Console.WriteLine("소유한 골드가 부족합니다.");
                        }
                        Console.WriteLine("원하시는 행동을 입력해주세요.");
                        Console.Write(">>");
                    }
                    else if (select == 0)
                    {
                        Menu(); // 메인 화면으로 이동
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
                }
            }
        }
    }
}
