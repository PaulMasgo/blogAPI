using System;

namespace BlogApi.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public bool Status { get; set; }
        public DateTime Modified { get; set; }
    }
}