using System.Collections.Generic;

namespace LinkHolder.Models{
    public class Link {
        public int Id { get; set; }
        public string Body { get; set; }
    }
    public class Folder {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Link> MyLinks { get; set; }
    }
}