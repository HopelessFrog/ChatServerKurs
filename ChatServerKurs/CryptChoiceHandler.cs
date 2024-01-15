using System.Reflection;

namespace ChatServerKurs;

public enum Crypts
{
    Caesar,
    PolybiusSquare,
    Verman,
    Non
}

public static class CryptChoiceHandler
{
    private static Crypts crypt;

    public static Crypts ChosenCrypt
    {
        get => crypt;
        set
        {
            crypt = value;

            if(crypt == Crypts.Non)
                return;
            try
            {
                var temp = Assembly.LoadFrom("Crypter.dll");
                var t = temp.GetTypes();
                Encrypt = t[0].GetMethod("Encrypt" + crypt);
                Decrypt = t[0].GetMethod("Decrypt" + crypt);

              
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("The server was launched without cryption\n");
                crypt = Crypts.Non;
                return;
            }
        }
    }

    public static MethodInfo Encrypt { get; set; }

    public static MethodInfo Decrypt { get; set; }
}