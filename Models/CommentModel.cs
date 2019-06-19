using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheWall.Models
{
    public class Comment
    {
        [Key]
        public int Id {get;set;}
        public int UserId {get;set;}
        public int MessageId {get;set;}
        public User User{get;set;}
        public Message Message{get;set;}
        [Required]
        public string Content {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}