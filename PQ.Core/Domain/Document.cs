using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQ.Core.Domain
{
    public class Document
    {
        public int PhilanthropicEntityId { get; set; }
        public string DocumentPath { get; set; }
        public byte[] DocumentData { get; set; }
        public PhilanthropicEntity PhilanthropicEntity { get; set; }
    }
}