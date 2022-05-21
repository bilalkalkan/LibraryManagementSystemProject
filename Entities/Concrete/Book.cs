using System;
using Core.Entities;

namespace Entities.Concrete
{
    public class Book:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public DateTime DateOfIssue { get; set; }
        public bool BookStatus { get; set; }
    }
}
