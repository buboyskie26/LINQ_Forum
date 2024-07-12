// See https://aka.ms/new-console-template for more information
using LINQ_Forum.Stackoverflow;


Console.WriteLine();
Console.WriteLine("Hello, World!");
Console.WriteLine("=============");
Console.WriteLine();
Console.WriteLine();


// Sample data
var forums = new List<Forum>
        {
            new Forum { ForumId = 1, Title = "Javscript Programming", ImageUrl = "javascript.png" },

            new Forum { ForumId = 2, Title = "C# Programming", ImageUrl = "csharp.png" },

            new Forum { ForumId = 3, Title = ".NET Framework", ImageUrl = ".netframework.png" }

        };

var posts = new List<Post>
        {
            new Post { PostId = 1, Title = "Welcome to the forum", Content = "Feel free to ask anything.", ForumId = 1, ApplicationUserId = 1 },

            new Post { PostId = 2, Title = "C# Tips on Forum 2", Content = "Share your best C# tips.", ForumId = 2, ApplicationUserId = 2 },

            new Post { PostId = 3, Title = "C# Hacks on Forum 2", Content = "Share your best C# Hacks.", ForumId = 2, ApplicationUserId = 2 }
        };


var postReplies = new List<PostReply>
        {
            new PostReply { PostReplyId = 1, Content = "Thanks!", PostId = 1, ApplicationUserId = 2 },
            new PostReply { PostReplyId = 1, Content = "Thanks again!", PostId = 1, ApplicationUserId = 2 },
            new PostReply { PostReplyId = 2, Content = "Use LINQ for better performance.", PostId = 2, ApplicationUserId = 1 }
        };


var postVotes = new List<PostVote>
        {
            new PostVote { PostVoteId = 1, IsUpVote = true, PostId = 1, ApplicationUserId = 2 },

            new PostVote { PostVoteId = 2, IsUpVote = true, PostId = 2, ApplicationUserId = 1 },

            new PostVote { PostVoteId = 3, IsUpVote = true, PostId = 1, ApplicationUserId = 3 },

            new PostVote { PostVoteId = 4, IsUpVote = true, PostId = 3, ApplicationUserId = 3 },

            new PostVote { PostVoteId = 5, IsUpVote = true, PostId = 3, ApplicationUserId = 1 },

            new PostVote { PostVoteId = 6, IsUpVote = true, PostId = 3, ApplicationUserId = 4 }
        };


var postReplyVotes = new List<PostReplyVote>
        {
            new PostReplyVote { PostReplyVoteId = 1, IsUpVote = true, PostReplyId = 1, ApplicationUserId = 1 },
            new PostReplyVote { PostReplyVoteId = 2, IsUpVote = false, PostReplyId = 2, ApplicationUserId = 2 }
        };


var users = new List<ApplicationUser>
        {
            new ApplicationUser { ApplicationUserId = 1, FirstName = "Justine Adrian", LastName = "Sirios" },
            new ApplicationUser { ApplicationUserId = 2, FirstName = "Jana", LastName = "Sirios" },
            new ApplicationUser { ApplicationUserId = 3, FirstName = "Hyper", LastName = "Sirios" },
            new ApplicationUser { ApplicationUserId = 4, FirstName = "Jane", LastName = "Doe" }
        };



//1.Retrieve All Posts in a Specific Forum

// Write a LINQ query to get all posts along WITH THEIR REPLIES for a given ForumId and its Forum Name.


var forumId = 1;

var query1 = from post in posts

             join f in forums

             on post.ForumId equals f.ForumId

             where post.ForumId == forumId

             select new
             {
                 Post = post,
                 ForumName = f.Title,
                 Replies = from r in postReplies

                           where r.PostId == post.PostId

                           select r

             };


/*foreach (var item in query1)
{
    Console.WriteLine($"Forum Name: {item.ForumName}");
    Console.WriteLine($"Post: {item.Post.Title}");

    foreach (var reply in item.Replies)
    {
        Console.WriteLine($"  Reply: {reply.Content}");
    }
}*/


/*2. Get Most Popular Post in Each Forum
 * Write a LINQ query to find the post with the highest number of upvotes in each forum.*/


var mostPopularPosts =

    from forum in forums

    let popularPost = (

        from post in posts

        join postVote in postVotes on post.PostId equals postVote.PostId into postVotesGroup

        where post.ForumId == forum.ForumId

        let upvoteCount = postVotesGroup.Count(v => v.IsUpVote)

        orderby upvoteCount descending

        select post

    ).FirstOrDefault()

    select new
    {
        ForumTitle = forum.Title ?? "",

        PostTitle = popularPost.Title,

        UpvoteCount = postVotes.Where(v => v.PostId == popularPost.PostId && v.IsUpVote == true).Count(),
    };

var query2 = from forum in forums

             let popularPost = (

                from post in posts

                join postVote in postVotes

                on post.PostId equals postVote.PostId into postVoteGroup

                where post.ForumId == forum.ForumId

                let voteCount = postVoteGroup.Count(w => w.IsUpVote)

                orderby voteCount descending

                select post

             ).FirstOrDefault()

             select new
             {
                 Forum = forum,
                 PostTitle = popularPost != null ? popularPost.Title : "No title",
                 UpvoteCount = popularPost != null ? postVotes.Where(w => w.IsUpVote && w.PostId == popularPost.PostId).Count() : 0
             };

foreach (var result in query2)
{
    Console.WriteLine($"Forum Title: {result.Forum.Title}, Post Title: {result.PostTitle}, Upvote Count: {result.UpvoteCount}");
}



// 3.Get Users with Most Upvotes on Their Posts
// Write a LINQ query to find the users who have received the most upvotes on their posts.

var usersWithMostUpvotes =

    from user in users

    join post in posts on user.ApplicationUserId equals post.ApplicationUserId

    join vote in postVotes on post.PostId equals vote.PostId

    where vote.IsUpVote

    group new { user, post } by new { user.ApplicationUserId, user.FirstName, user.LastName, post.Title } into userGroup

    //group new { user } by new { user.ApplicationUserId, user.FirstName, user.LastName } into userGroup

    let upvoteCount = userGroup.Count()

    orderby upvoteCount descending

    select new
    {
        UserId = userGroup.Key.ApplicationUserId,

        FullName = userGroup.Key.FirstName + " " + userGroup.Key.LastName,

        UpvoteCount = upvoteCount,

        PostTitle = userGroup.Key.Title
    };


foreach (var user in usersWithMostUpvotes)
{
    //Console.WriteLine($"User: {user.FullName}, Upvotes: {user.UpvoteCount}");

    Console.WriteLine($"User: {user.FullName}, Post: {user.PostTitle}, Upvotes: {user.UpvoteCount}");
}


/*4. Find Forums with No Posts
Write a LINQ query to find all forums that do not have any posts.*/

/*5. Get Posts with Specific Keywords
Write a LINQ query to find all posts that contain specific keywords in their title or content.*/



/*6. Count Replies Per Post
Write a LINQ query to count the number of replies each post has.*/

/*7. Get Users with Most Replies
Write a LINQ query to find the users who have posted the most replies.*/

/*8. Aggregate Votes for Each Post and Reply
Write a LINQ query to aggregate the total number of upvotes and downvotes for each post and reply.*/

/*9. Find Users with Most Votes (Upvotes and Downvotes Combined)
Write a LINQ query to find the users who have received the most combined upvotes and downvotes on their posts and replies.*/

/*10. Find Posts with No Upvotes or Downvotes
Write a LINQ query to find all posts that have neither upvotes nor downvotes.*/

/*11. Get Top 5 Most Active Forums
Write a LINQ query to find the top 5 forums with the most posts.*/

/*12. Retrieve Posts and Replies by a Specific User
Write a LINQ query to get all posts and replies made by a specific user.*/

/*13. Get Replies to a Specific Post by a Specific User
Write a LINQ query to find all replies to a specific post made by a specific user.*/

/*14. Find Users Who Have Not Posted
Write a LINQ query to find all users who have not posted any posts or replies.*/

/*15. Get Posts Created within a Specific Date Range
Write a LINQ query to find all posts created within a specific date range.*/


Console.ReadLine();


