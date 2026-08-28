// https://github.com/rauald/mp15-FirstTest

using System;


class Program
{
    const string SHOP_NAME = "메플 분식집";
    const string SHOPPING_CART = "장바구니";
    const string MENU_BOARD = "메뉴판";

    static void Main(string[] args)
    {

        // 가게 : 분식집
        // 분류 : 떡류, 튀김류, 김밥류, 꼬치류
        // 메뉴(8종) : 일반 떡볶이, 로제 떡볶이, 오뎅, 매운 오뎅, 김말이 튀김, 오징어 튀김, 야채 김밥, 참치 김밥

        int TotalOrder = 0;
        int TotalSales = 0;

        Menu[] menu = new Menu[]
        {
            new Tteokbokki(CategoryType.RiceCakes, "떡볶이", 4000),
            new RoseTteokbokki(CategoryType.RiceCakes, "로제 떡볶이", 5000),
            new FriedSeaweedRoll(CategoryType.Fry, "김말이 튀김", 500),
            new FriedSquid(CategoryType.Fry, "오징어 튀김", 1000),
            new Oden(CategoryType.Skewer, "오뎅", 500),
            new SpicyFishCakes(CategoryType.Skewer, "빨간 오뎅", 700),
            new VegetableKimbap(CategoryType.Kimbap, "야채 김밥", 4000),
            new TunaKimbap(CategoryType.Kimbap, "참치 김밥", 5000),
        };

        List<int> OrderMenuList = new();
        List<int> OrderCntList = new();

        int choiceIdx = 0;

        while(true)
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"{SHOP_NAME}");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"[{SHOPPING_CART}]");

            int totalPrice = 0;

            int totalSkewer = 0;

            for (int i = 0; i < OrderMenuList.Count; i++)
            {
                if (menu[OrderMenuList[i]].Category == CategoryType.Skewer)
                {
                    totalSkewer += OrderCntList[i];
                }
            }

            for (int i = 0; i < OrderMenuList.Count; i++)
            {
                Console.WriteLine($"  {menu[OrderMenuList[i]].Name} x{OrderCntList[i]}   {menu[OrderMenuList[i]].MenuCalculate(totalSkewer, OrderCntList[i])}원");
                totalPrice += menu[OrderMenuList[i]].MenuCalculate(totalSkewer, OrderCntList[i]);
            }

            Console.WriteLine($"  합계 : {totalPrice}원");
            Console.WriteLine("----------------------------------------");


            Console.WriteLine();

            for(int i = 0; i < (int)CategoryType.Max; i++)
            {
                Console.Write($"{i + 1}. {CategoryName((CategoryType)i)}  ");
            }

            Console.WriteLine();

            // 1부터 4 사이의 번호를 받습니다. 숫자가 아니거나 범위를 벗어나면 다시 묻습니다.
            int categoryChoiceNum = ConsoleInput.ReadIntInRange("선택 번호 : ", 1, 4);

            choiceIdx = (categoryChoiceNum - 1);

            List<Menu> choiceList = new();

            for(int i = 0; i < menu.Length; i++)
            {
                if (menu[i].Category == (CategoryType)choiceIdx)
                {
                    choiceList.Add(menu[i]);
                }
            }

            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"[{MENU_BOARD}]");
            for (int i = 0; i < choiceList.Count; i++)
            {
                string saleStr = "";
                if (choiceList[i].IsSale)
                {
                    saleStr = choiceList[i].PrintSale();
                }
                Console.WriteLine($" {i + 1}. ({CategoryName(choiceList[i].Category)})  {choiceList[i].Name}  {choiceList[i].Price}원  {(choiceList[i].IsSale ? saleStr : "[정가]")}");
            }
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine("1. 담기   2. 전체 비우기   3. 결제   4. 영업 종료");

            // 1부터 4 사이의 번호를 받습니다. 숫자가 아니거나 범위를 벗어나면 다시 묻습니다.
            int ChoiceNum = ConsoleInput.ReadIntInRange("선택 번호 : ", 1, 4);

            Console.WriteLine();
            if (ChoiceNum == 1)
            {
                // 1부터 2 사이의 번호를 받습니다. 숫자가 아니거나 범위를 벗어나면 다시 묻습니다.
                int menuNumber = ConsoleInput.ReadIntInRange("메뉴 번호 : ", 1, 2);
                int menuCnt = ConsoleInput.ReadIntInRange("주문 수량 : ", 1, 99);

                if (OrderMenuList.Contains(choiceIdx * 2 + menuNumber - 1))
                {
                    for(int i =0; i < OrderMenuList.Count; i++)
                    {
                        if(OrderMenuList[i] == (choiceIdx * 2 + menuNumber - 1))
                        {
                            OrderCntList[i] += menuCnt;
                        }
                    }
                }
                else
                {
                    OrderMenuList.Add(choiceIdx * 2 + menuNumber - 1);
                    OrderCntList.Add(menuCnt);
                }
            }
            else if (ChoiceNum == 2)
            {
                OrderMenuList.Clear();
                OrderCntList.Clear();
            }
            else if (ChoiceNum == 3)
            {
                // 0 이상의 값을 받습니다. 위쪽 한계를 정하기 어려울 때 씁니다.
                int paid = ConsoleInput.ReadIntAtLeast("받은 금액 : ", 0);

                if(paid < totalPrice)
                {
                    Console.WriteLine("돈이 모자랍니다!");
                }
                else
                {
                    int sales = 0;
                    sales = paid - totalPrice;
                    Console.WriteLine($"감사합니다! 거스름돈 {sales}원 입니다.");
                    TotalOrder++;
                    TotalSales += totalPrice;

                    OrderMenuList.Clear();
                    OrderCntList.Clear();
                }
            }
            else if (ChoiceNum == 4)
            {
                Console.WriteLine("영업을 종료하겠습니다.");
                break;
            }

            // 결과를 보여 준 뒤 화면을 지우기 전에 잠시 멈춥니다.
            ConsoleInput.Pause();

            Console.WriteLine("----------------------------------------");
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"총 주문 건수 : {TotalOrder}  개");
        Console.WriteLine($"총 매출 : {TotalSales}  원");
    }

    static string CategoryName(CategoryType category)
    {
        string categoryName = "";

        switch (category)
        {
            case CategoryType.RiceCakes:
                categoryName = "분식류";
                break;
            case CategoryType.Fry:
                categoryName = "튀김류";
                break;
            case CategoryType.Skewer:
                categoryName = "꼬치류";
                break;
            case CategoryType.Kimbap:
                categoryName = "김밥류";
                break;
        }

        return categoryName;
    }
}