using System;

class Program
{
    static void Main(string[] args)
    {
        // 가게 : 분식집
        // 분류 : 떡류, 튀김류, 김밥류, 꼬치류
        // 메뉴(8종) : 일반 떡볶이, 로제 떡볶이, 오뎅, 매운 오뎅, 김말이 튀김, 오징어 튀김, 야채 김밥, 참치 김밥









        // 1부터 6 사이의 번호를 받습니다. 숫자가 아니거나 범위를 벗어나면 다시 묻습니다.
        int menuNumber = ConsoleInput.ReadIntInRange("메뉴 번호 : ", 1, 6);

        // 0 이상의 값을 받습니다. 위쪽 한계를 정하기 어려울 떄 씁니다.
        int paid = ConsoleInput.ReadIntAtLeast("받은 금액 : ", 0);

        // 결과를 보여 준 뒤 화면을 지우기 전에 잠시 멈춥니다.
        ConsoleInput.Pause();
    }
}