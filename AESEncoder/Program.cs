using AESEncoder;

Console.WriteLine("輸入密碼:");

var p1 = Console.ReadLine();

if (!string.IsNullOrEmpty(p1))
{
    var c = new Code();
    var result = c.Encoder(p1);

    Console.WriteLine("Key：" + result.Key + "\n" + "IV：" + result.IV + "\n" + "密碼：" + result.Pwd);
}