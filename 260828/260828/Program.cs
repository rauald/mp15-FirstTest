using System;

class Program
{
    static void Main(string[] args)
    {
        const string SHOP_NAME = "메플 분식집";
        const int SALE_FRY_COUNT = 3;
        const int SALE_ODEN_COUNT = 5;
        const float SALE_PRICE = 10f;

        // 가게 : 분식집
        // 분류 : 떡류, 튀김류, 김밥류, 꼬치류
        // 메뉴(8종) : 일반 떡볶이, 로제 떡볶이, 오뎅, 매운 오뎅, 김말이 튀김, 오징어 튀김, 야채 김밥, 참치 김밥

        int TotalOrder = 0;
        int TotalSales = 0;

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("메플 분식집");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        Menu[] menu = new Menu[]
        {
            new Tteokbokki("분식류", "떡볶이", 4000),
            new RoseTteokbokki("분식류", "로제 떡볶이", 5000),
        };

        Console.WriteLine("[메뉴판]");
        for(int i = 0; i < menu.Length; i++)
        {
            Console.WriteLine($" {i + 1}. ({menu[i].Category})  {menu[i].Name}  {menu[i].Price}원");
        }
        Console.WriteLine("----------------------------------------");

        List<int> OrdermenuList = new();
        List<int> OrderCntList = new();

        while(true)
        {
            Console.WriteLine();
            Console.WriteLine("[장바구니]");
            int TotalPrice = 0;
            for(int i = 0;  i < OrdermenuList.Count; i++)
            {
                Console.WriteLine($"  {menu[OrdermenuList[i]].Name} x{OrderCntList[i]}   { menu[OrdermenuList[i]].MenuCalculate(OrderCntList[i])}원");
                TotalPrice += menu[OrdermenuList[i]].MenuCalculate(OrderCntList[i]);
            }

            Console.WriteLine($"  합계 : {TotalPrice}원");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine("1. 담기   2. 전체 비우기   3. 결제   4. 영업 종료");

            // 1부터 6 사이의 번호를 받습니다. 숫자가 아니거나 범위를 벗어나면 다시 묻습니다.
            int ChoiceNum = ConsoleInput.ReadIntInRange("선택 번호 : ", 1, 4);

            Console.WriteLine();
            if (ChoiceNum == 1)
            {
                // 1부터 6 사이의 번호를 받습니다. 숫자가 아니거나 범위를 벗어나면 다시 묻습니다.
                int menuNumber = ConsoleInput.ReadIntInRange("메뉴 번호 : ", 1, 8);
                int menuCnt = ConsoleInput.ReadIntInRange("주문 수량 : ", 1, 99);

                OrdermenuList.Add(menuNumber - 1);
                OrderCntList.Add(menuCnt);
            }
            else if (ChoiceNum == 2)
            {
                OrdermenuList.Clear();
                OrderCntList.Clear();
            }
            else if (ChoiceNum == 3)
            {
                // 0 이상의 값을 받습니다. 위쪽 한계를 정하기 어려울 때 씁니다.
                int paid = ConsoleInput.ReadIntAtLeast("받은 금액 : ", 0);

                if(paid < TotalPrice)
                {
                    Console.WriteLine("돈이 모자랍니다!");
                }
                else
                {
                    Console.WriteLine($"감사합니다! 거스름돈 {paid - TotalPrice}원 입니다.");
                    TotalOrder++;
                    TotalSales += paid;

                    OrdermenuList.Clear();
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
    }
}