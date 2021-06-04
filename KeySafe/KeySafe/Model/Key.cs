namespace KeySafe.Model
{
    public class Key
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string Notes { get; set; }
        public int DirectoryID { get; set; }
    }
}
