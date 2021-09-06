using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class ProjectCommunication : BaseModel
    {
        public int ProjectCommunicationId { get; set; }
        public int ProjectId { get; set; }
        [Required, StringLength(256), Display(Name = "Subject")]

        public string Subject { get; set; }
        [Required, DataType(DataType.DateTime), Display(Name = "Communication Date")]
        public DateTime CommunicationDate { get; set; } = DateTime.UtcNow;

        [Required, StringLength(2000)]
        public string Communication { get; set; }

    }
}
