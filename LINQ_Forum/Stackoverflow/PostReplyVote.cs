using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Forum.Stackoverflow
{
    public class PostReplyVote
    {

        public int PostReplyVoteId { get; set; }

        public bool IsUpVote { get; set; } // True for upvote, false for downvote


        public int PostReplyId { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
