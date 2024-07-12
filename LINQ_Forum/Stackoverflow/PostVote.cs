using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Forum.Stackoverflow
{
    public class PostVote
    {
        public int PostVoteId { get; set; }

        public bool IsUpVote { get; set; } // True for upvote, false for downvote


        public int PostId { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
