namespace ChatServerKurs
{
    public enum Crypts {
        Caesar,
        PolybiusSquare,
        Verman,
        Non
    }
    public static class CryptChoiceHandler
    {
        public static Crypts ChosenCrypt { get; set; }
    }
}
