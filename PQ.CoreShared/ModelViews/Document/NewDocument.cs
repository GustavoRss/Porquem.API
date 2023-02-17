using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace PQ.CoreShared.ModelViews
{
    public class NewDocument
    {
        /// <summary>
        /// Documento da entidade: Certidão Negativa, CNPJ
        /// </summary>
        /// <example>123456789</example>
        /// 
        public string DocumentPath { get; set; }
        public byte[] DocumentData { get; set; }

    }
}