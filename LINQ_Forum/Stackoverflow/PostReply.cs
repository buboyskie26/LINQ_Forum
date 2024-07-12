using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Forum.Stackoverflow
{
    public class PostReply
    {
        public int PostReplyId { get; set; }
        public string Content { get; set; } = string.Empty;


        public int PostId { get; set; }

        public int ApplicationUserId { get; set; }
    }
}
