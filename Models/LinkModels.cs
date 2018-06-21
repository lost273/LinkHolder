using System.Collections.Generic;

namespace LinkHolder.Models{
    public class Link {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public int FolderId { get; set; }
        public Folder Folder { get; set; }
    }
    public class Folder {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Link> MyLinks { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
    public class SaveLinkModel {
        public string LinkBody { get; set; }
        public string LinkDescription { get; set; }
        public string FolderName { get; set; }
    }
    public sealed class ViewFolder {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ViewLink> MyLinks { get; set; }
    }
    public sealed class ViewLink {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
    }
}