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

        // 총 매출
        int TotalOrder = 0;
        // 총 주문량
        int TotalSales = 0;

        // 메뉴 담기
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

        // 장바구니에 담긴 메뉴 번호
        List<int> OrderMenuList = new();
        // 장바구니에 담긴 메뉴 주문 개수
        List<int> OrderCntList = new();

        // 두개의 리스트의 인덱스를 통해 주문과 주문량을 확인한다.
        // ex) OrderMenuList = {0, 3, 1, 2};
        // ex) OrderCntList = {3, 5, 11, 3};
        // 0번째 메뉴 - 0번 메뉴 / 3개 주문
        // 1번째 메뉴 - 3번 메뉴 / 5개 주문
        // 이어서 ...

        // 현재 선택한 카테고리 저장을 위해 만든 변수
        // 메뉴를 장바구니에 넣으면 화면이 리셋 되는데 그때 그 전에 고른 카테고리를 보여주기 위해 넣은 변수
        int choiceIdx = 0;

        while(true)
        {
            // 화면 초기화
            Console.Clear();
            
            // 상점 이름
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"{SHOP_NAME}");
            Console.WriteLine("----------------------------------------");
            
            Console.WriteLine();
            
            // 장바구니
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"[{SHOPPING_CART}]");

            // 현재 주문(장바구니) 값의 합
            int totalPrice = 0;
            // 꼬치류 총 개수 (테스트 추가1 - 종류에 따라 할인을 위해 알기 위한 변수)
            int totalSkewer = 0;

            // 꼬치류 개수 확인
            for (int i = 0; i < OrderMenuList.Count; i++)
            {
                if (menu[OrderMenuList[i]].Category == CategoryType.Skewer)
                {
                    totalSkewer += OrderCntList[i];
                }
            }

            // 장바구니에 담긴 메뉴 출력
            for (int i = 0; i < OrderMenuList.Count; i++)
            {
                Console.WriteLine($"  {menu[OrderMenuList[i]].Name} x{OrderCntList[i]}   {menu[OrderMenuList[i]].MenuCalculate(totalSkewer, OrderCntList[i])}원");
                totalPrice += menu[OrderMenuList[i]].MenuCalculate(totalSkewer, OrderCntList[i]);
            }

            // 장바구니에 담긴 계산 합
            Console.WriteLine($"  합계 : {totalPrice}원");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            // 종류 선택지 (테스트 추가2 - 종류(카테고리) 선택지)
            for(int i = 0; i < (int)CategoryType.Max; i++)
            {
                Console.Write($"{i + 1}. {CategoryName((CategoryType)i)}  ");
            }

            Console.WriteLine();

            // 1부터 4 사이의 번호를 받습니다. 숫자가 아니거나 범위를 벗어나면 다시 묻습니다.
            int categoryChoiceNum = ConsoleInput.ReadIntInRange("선택 번호 : ", 1, 4);

            // 받은 번호에 -1을 한 이유는 카테고리(Enum 열거형을 0부터 시작했기 때문에)
            choiceIdx = (categoryChoiceNum - 1);

            // 고른 카테고리에 맞는 메뉴를 보여주기 위한 리스트
            List<Menu> choiceList = new();

            // 해당 카테고리에 맞는 메뉴를 리스트에 넣기
            for(int i = 0; i < menu.Length; i++)
            {
                if (menu[i].Category == (CategoryType)choiceIdx)
                {
                    choiceList.Add(menu[i]);
                }
            }

            Console.WriteLine();

            // 메뉴판
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"[{MENU_BOARD}]");

            // 카테고리에 맞는 메뉴를 넣은 리스트를 출력
            for (int i = 0; i < choiceList.Count; i++)
            {
                string saleStr = "";
                // 할인을 하는지 안하는지 구분
                if (choiceList[i].IsSale)
                {
                    // 할인하는 메뉴 부가 설명 붙이기 위한 string
                    saleStr = choiceList[i].PrintSale();
                }
                // 메뉴 / 가격 / 할인 조건 출력
                Console.WriteLine($" {i + 1}. ({CategoryName(choiceList[i].Category)})  {choiceList[i].Name}  {choiceList[i].Price}원  {(choiceList[i].IsSale ? saleStr : "[정가]")}");
            }
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            // 받을 번호 내용 출력
            Console.WriteLine("1. 담기   2. 전체 비우기   3. 결제   4. 영업 종료");
            // 1부터 4 사이의 번호를 받습니다. 숫자가 아니거나 범위를 벗어나면 다시 묻습니다.
            int ChoiceNum = ConsoleInput.ReadIntInRange("선택 번호 : ", 1, 4);

            Console.WriteLine();

            // 행동 선택 구분
            // 담기
            if (ChoiceNum == 1)
            {
                // 1부터 2 사이의 번호를 받습니다. 숫자가 아니거나 범위를 벗어나면 다시 묻습니다.
                // 담을 메뉴 번호
                int menuNumber = ConsoleInput.ReadIntInRange("메뉴 번호 : ", 1, 2);
                // 담을 메뉴 주문량
                int menuCnt = ConsoleInput.ReadIntInRange("주문 수량 : ", 1, 99);

                // 해당 메뉴가 이미 장바구니에 포함 되어있는지 확인
                // 카테고리번호 * 2 + 메뉴 번호 - 1 | 을 한 이유는 카테고리별로 메뉴가 나오기때문에
                // 메뉴 번호를 0과 1로 하면 menu 배열의 0 과 1번만 들어가기 때문
                if (OrderMenuList.Contains(choiceIdx * 2 + menuNumber - 1))
                {
                    // 이미 메뉴가 있다면
                    for(int i =0; i < OrderMenuList.Count; i++)
                    {
                        // 해당 메뉴의 주문량 추가
                        if(OrderMenuList[i] == (choiceIdx * 2 + menuNumber - 1))
                        {
                            OrderCntList[i] += menuCnt;
                        }
                    }
                }
                // 메뉴가 없다면 추가하고 주문량도 추가한다.
                else
                {
                    OrderMenuList.Add(choiceIdx * 2 + menuNumber - 1);
                    OrderCntList.Add(menuCnt);
                }
            }
            // 전체 비우기
            else if (ChoiceNum == 2)
            {
                // 담겨져 있는 메뉴 와 주문량을 전체 비운다.
                OrderMenuList.Clear();
                OrderCntList.Clear();
            }
            // 결제
            else if (ChoiceNum == 3)
            {
                // 0 이상의 값을 받습니다. 위쪽 한계를 정하기 어려울 때 씁니다.
                int paid = ConsoleInput.ReadIntAtLeast("받은 금액 : ", 0);

                // 받은 금액이 결제 해야할 금액보다 적다면
                if(paid < totalPrice)
                {
                    Console.WriteLine("돈이 모자랍니다!");
                }
                // 같거나 많다면
                else
                {
                    // 결제 할 금액과 거스름 돈 확인 및 출력
                    int sales = 0;
                    sales = paid - totalPrice;
                    Console.WriteLine($"감사합니다! 거스름돈 {sales}원 입니다.");
                    // 총 주문량 추가
                    TotalOrder++;
                    // 총 매출 추가 (이때 매출은 받은 금액이 아닌 결제된 금액을 넣어야 한다.
                    TotalSales += totalPrice;

                    // 결제가 완료 되었으니 장바구니 초기화
                    OrderMenuList.Clear();
                    OrderCntList.Clear();
                }
            }
            // 영업 종료
            else if (ChoiceNum == 4)
            {
                Console.WriteLine("영업을 종료하겠습니다.");
                // 영업 종료시 while 나가기
                break;
            }

            // 결과를 보여 준 뒤 화면을 지우기 전에 잠시 멈춥니다.
            ConsoleInput.Pause();

            Console.WriteLine("----------------------------------------");
        }

        // 총 주문량 | 총 매출 출력
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"총 주문 건수 : {TotalOrder}  개");
        Console.WriteLine($"총 매출 : {TotalSales}  원");
    }

    // 카테고리에 따라 받을 string
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