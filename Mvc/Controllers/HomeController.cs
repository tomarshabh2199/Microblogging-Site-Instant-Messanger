using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data.Entity.Infrastructure;

namespace Mvc.Controllers
{

    [Authorize]
    //[Route("/api/home")]
    public class HomeController : Controller
    {
        private Entities Db = new Entities();
        public ActionResult Index(string loginUserEmail)
        {
            using (var context = new Entities())
            {
                Detail loginUserData = context.Details.Where(usr => usr.Email == loginUserEmail).FirstOrDefault();

                string imgDataURL = string.Format("data:image/jpg;base64,{0}", loginUserData.Image);

                Home homedata = new Home();
                homedata.userImage = imgDataURL;
                var postData = context.ComposePosts.OrderByDescending(usr => usr.Created_date).ToList();

                List<Models.ComposePost> ComposePosts = new List<Models.ComposePost>();
                foreach (var data in postData)
                {
                    Models.ComposePost postdata = new Models.ComposePost();
                    postdata.Id = data.Id;
                    postdata.Content = data.Content;
                    postdata.Name = data.Name;
                    Detail postUserDetails = context.Details.Where(usr => usr.Id == data.User_id).FirstOrDefault();

                    string postUserImgDataURL = string.Format("data:image/jpg;base64,{0}", postUserDetails.Image);


                    postdata.userImage = postUserImgDataURL;
                    postdata.User_id = data.User_id.ToString();
                    if (data.User_id == loginUserData.Id)
                    {
                        postdata.isUserTweet = true;
                    }
                    else {
                        postdata.isUserTweet = false;
                    }
                    postdata.Created_date = data.Created_date.ToString();
                    ComposePosts.Add(postdata);


                }
                string sqlQuery = "SELECT count(*) FROM [CRUDDB].[dbo].[followers] WHERE user_id = " + loginUserData.Id;
                var totalFollowerCount = context.Database.SqlQuery<Int32>(sqlQuery).ToList();

                string sqlQuery2 = "SELECT count(*) FROM [CRUDDB].[dbo].[followings] WHERE user_id = " + loginUserData.Id;
                var totalFollowingCount = context.Database.SqlQuery<Int32>(sqlQuery2).ToList();

                homedata.ComposePosts = ComposePosts;
                homedata.totalFollowerCount = totalFollowerCount[0];
                homedata.totalFollowingCount = totalFollowingCount[0];
                return View(homedata);
            }

        }

        public ActionResult Followers(string loginEmailAddress)
        {
            ViewBag.Message = "Your Followers page.";
            using (var context = new Entities())
            {

                var UserData = context.Details.Where(usr => usr.Email == loginEmailAddress).FirstOrDefault();

                var loginUserfollowerData = context.followers.Where(usr => usr.User_id == UserData.Id).ToList();
                List<Models.FollowerDetailsView> FollowerData = new List<Models.FollowerDetailsView>();
                foreach (var data in loginUserfollowerData)
                {
                    Models.FollowerDetailsView followerData = new Models.FollowerDetailsView();
                    //followerData.Id = data.User_id;
                    var follwerUserImg = context.Details.Where(usr => usr.Id == data.Follower_id).FirstOrDefault();
                    string imgDataURL = string.Format("data:image/jpg;base64,{0}", follwerUserImg.Image);

                    Home homedata = new Home();
                    followerData.userImage = imgDataURL;
                    followerData.Firstname = follwerUserImg.Firstname;

                    FollowerData.Add(followerData);


                }

                return View(FollowerData);
            }
        }

        [HttpPost]
        [Route("api/save-follow-data")]
        public String GetFollowData(string data, String loginUserEmail)
        {
            using (var context = new Entities())
            {
                int result = Int16.Parse(data);

                Detail ctUser = context.Details.Where(usr => usr.Id == result).FirstOrDefault();

                Detail loginUserData = context.Details.Where(usr => usr.Email == loginUserEmail).FirstOrDefault();
                if (ctUser != null)
                {
                    follower follow = new follower();
                    follow.Follower_id = loginUserData.Id;
                    follow.Follower_name = loginUserData.Firstname;
                    follow.User_id = ctUser.Id;
                    context.followers.Add(follow);
                    context.SaveChanges();

                    following followingData = new following();
                    followingData.Following_id = ctUser.Id;
                    followingData.Following_name = ctUser.Firstname;
                    followingData.User_id = loginUserData.Id;
                    context.followings.Add(followingData);
                    context.SaveChanges();

                    return "User Follow Successfully";
                }
                else {
                    return "UnSuccessful Operation";
                }
            }
        }

        [HttpPost]
        [Route("api/delete-unfollow-data")]
        public String DeleteFollowData(string data, String loginUserEmail)
        {
            using (var context = new Entities())
            {
                int result = Int16.Parse(data);
                Detail ctUser = context.Details.Where(usr => usr.Id == result).FirstOrDefault();
                if (ctUser != null)
                {
                    Detail loginUserData = context.Details.Where(usr => usr.Email == loginUserEmail).FirstOrDefault();
                    if (loginUserData != null) {
                        context.followers.Remove(context.followers.Single(a => a.Follower_id == result && a.User_id == loginUserData.Id));
                        context.SaveChanges();
                        return "User UnFollow Successfully";
                    }

                    else {
                        return "UnSuccessful Operation";
                    }
                }
                else
                {
                    return "UnSuccessful Operation";
                }
            }
        }



        public ActionResult Following(string loginEmailAddress)
        {
            ViewBag.Message = "Your Following page.";

            using (var context = new Entities())
            {
                var UserData = context.Details.Where(usr => usr.Email == loginEmailAddress).FirstOrDefault();

                var loginUserfollowingData = context.followings.Where(usr => usr.User_id == UserData.Id).ToList();
                List<Models.FollowerDetailsView> FollowingData = new List<Models.FollowerDetailsView>();
                foreach (var data in loginUserfollowingData)
                {
                    Models.FollowerDetailsView followingData = new Models.FollowerDetailsView();
                    followingData.Id = data.Following_id;
                    var followingUserImg = context.Details.Where(usr => usr.Id == data.Following_id).FirstOrDefault();
                    string imgDataURL = string.Format("data:image/jpg;base64,{0}", followingUserImg.Image);

                    Home homedata = new Home();
                    followingData.userImage = imgDataURL;
                    followingData.Firstname = data.Following_name;

                    FollowingData.Add(followingData);
                }
                return View(FollowingData);
            }
        }


        [HttpPost]
        [Route("api/save-following-data")]
        public String SaveFollowingData(string data, String loginUserEmail)
        {
            using (var context = new Entities())
            {
                int result = Int16.Parse(data);

                Detail ctUser = context.Details.Where(usr => usr.Id == result).FirstOrDefault();

                Detail loginUserData = context.Details.Where(usr => usr.Email == loginUserEmail).FirstOrDefault();
                if (ctUser != null && loginUserData != null)
                {
                    following followingData = new following();
                    followingData.Following_id = ctUser.Id;
                    followingData.Following_name = ctUser.Firstname;
                    followingData.User_id = loginUserData.Id;
                    context.followings.Add(followingData);
                    context.SaveChanges();
                    return "User Following Successfully";
                }
                else
                {
                    return "UnSuccessful Operation";
                }
            }
        }

        [HttpPost]
        [Route("api/delete-unfollowing-data")]
        public String DeleteFollowingData(string data, String loginUserEmail)
        {
            using (var context = new Entities())
            {
                int result = Int16.Parse(data);
                Detail loginUserData = context.Details.Where(usr => usr.Email == loginUserEmail).FirstOrDefault();
                if (loginUserData != null)
                {
                    context.followings.Remove(context.followings.Single(a => a.Following_id == result && a.User_id == loginUserData.Id));
                    context.SaveChanges();

                    context.followers.Remove(context.followers.Single(a => a.Follower_id == loginUserData.Id && a.User_id == result));
                    context.SaveChanges();

                    return "User UnFollow Successfully";
                }

                else
                {
                    return "UnSuccessful Operation";
                }
            }
        }


        public ActionResult Search()
        {
            ViewBag.Message = "Your Search page.";

            return View();
        }



        public ActionResult SearchData(string data, string loginUserEmail)
        {
            ViewBag.Message = "Your Search Data.";
            using (var context = new Entities())
            {
                Detail loginUserData = context.Details.Where(usr => usr.Email == loginUserEmail).FirstOrDefault();
                List<Detail> ctUsers = context.Details.Where(usr => usr.Email != loginUserEmail && usr.Firstname.Contains(data)).ToList();
                List<Models.FollowerDetailsView> SearchData = new List<Models.FollowerDetailsView>();
                foreach (var detail in ctUsers)
                {
                    Models.FollowerDetailsView searchDetails = new Models.FollowerDetailsView();
                    searchDetails.Id = detail.Id;
                    var loginUserfollowerData = context.followings.Where(usr => usr.User_id == loginUserData.Id && usr.Following_id == detail.Id).FirstOrDefault();
                    if (loginUserfollowerData != null) {
                        searchDetails.isUserFollow = true;
                    }
                    else {
                        searchDetails.isUserFollow = false;
                    }
                    string imgDataURL = string.Format("data:image/jpg;base64,{0}", detail.Image);

                    Home homedata = new Home();
                    searchDetails.userImage = imgDataURL;
                    searchDetails.Firstname = detail.Firstname;

                    SearchData.Add(searchDetails);


                }
                return View("ViewSearchData", SearchData);
            }
        }


        public ActionResult Analytics()
        {
                ViewBag.Message = "Anayltics";

            List<AnalyticsViewModel> analyticsViewModel = new List<AnalyticsViewModel>();

            using (var context = new Entities())
            {
                DateTime today = DateTime.Today;
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd");
                string sqlQuery = "SELECT count(*) FROM [CRUDDB].[dbo].[ComposePosts] WHERE Created_date between '" + dateTime + " 00:00:00' AND '" + dateTime + " 23:59:59'";
                var totalCount = context.Database.SqlQuery<Int32>(sqlQuery).ToList();
                var trendinghashtag= context.Database.SqlQuery<string>("select t.Name FROM ( select Name, COUNT(1) AS count, RANK() OVER(ORDER BY COUNT(1) DESC) rank_no FROM[CRUDDB].[dbo].[ComposePosts] GROUP BY Name ) AS t WHERE rank_no = 1").ToList<string>();

                string query1 = " select count(user_id) as Data , user_id from [CRUDDB].[dbo].[ComposePosts] Group BY User_id Order by Data Desc;";
                
                var maximumtweetperson = context.Database.SqlQuery<MostTweetData>(query1).ToList<MostTweetData>();
                AnalyticsViewModel data = new AnalyticsViewModel();
                foreach (var tweetDetail in maximumtweetperson) {
                    var userId = tweetDetail.user_id;
                    string query2 = "Select Firstname from [CRUDDB].[dbo].Details where id =" + userId;
                    var mostTweetedPerson = context.Database.SqlQuery<string>(query2).ToList<string>();
                    data.MostTweetByPerson = mostTweetedPerson[0].ToString();
                    break;
                }
                data.TotalTweetstoday = totalCount[0].ToString();
                data.MostTrendingHashTags = trendinghashtag[0].ToString();
                

                analyticsViewModel.Add(data);
                context.SaveChanges();
        }
            return View("Analytics", analyticsViewModel);
     } 

[HttpGet]
[Route("api/get-analytics-data")]
public String getSearchData()
{
    using (var context = new Entities())
    {
        return ("");
               
    }
}

public ActionResult ComposePost()
{
    ViewBag.Message = "compose new post";

    return View();
}

[HttpPost]
public ActionResult ComposePost(Mvc.Models.ComposePost model)
{
    //using db entities
    using (var context = new Entities())
    {
        Detail loginUserData = context.Details.Where(usr => usr.Email == model.User_id).FirstOrDefault();
        if (loginUserData != null)
        {
                
        ComposePost data = new ComposePost();
        data.Content = model.Content;
        data.Name = model.Name;
        data.User_id = loginUserData.Id;
        data.Created_date = DateTime.Now;
        context.ComposePosts.Add(data);
        context.SaveChanges();
    }
    }
    return RedirectToAction("Index", "Home", new { loginUserEmail = model.User_id });
}



public ActionResult EditPostData(string postId, String UserId)
{
    using (var context = new Entities())
    {
        int postDataId = Int16.Parse(postId);
        int PostUserId = Int16.Parse(UserId);
        Detail ctUser = context.Details.Where(usr => usr.Id == PostUserId).FirstOrDefault();

        var postData = context.ComposePosts.Where(a => a.Id == postDataId && a.User_id == PostUserId).ToList();
        return View("EditPost", postData);



    }
}

//[HttpPost]
//[Route("api/edit-post-data")]
//public ActionResult EditPostData(string postId, String userId)
//{
//    using (var context = new Entities())
//    {
//        int postDataId = Int16.Parse(postId);
//        int PostUserId = Int16.Parse(userId);
//        Detail ctUser = context.Details.Where(usr => usr.Id == PostUserId).FirstOrDefault();
                
//            var postData = context.ComposePosts.Where(a => a.Id == postDataId && a.User_id == PostUserId).ToList();
//            return View("EditPost",postData);
                   

                
//    }
//}



[HttpPost]
[Route("api/delete-post-data")]
public String DeletePostData(string postId, String userId)
{
    using (var context = new Entities())
    {
        int postDataId = Int16.Parse(postId);
        int PostUserId = Int16.Parse(userId);
        Detail ctUser = context.Details.Where(usr => usr.Id == PostUserId).FirstOrDefault();
        if (ctUser != null)
        {
                context.ComposePosts.Remove(context.ComposePosts.Single(a => a.Id == postDataId && a.User_id == PostUserId));
                context.SaveChanges();
                return "Post Delete Successfully";
                  
        }
        else
        {
            return "UnSuccessful Operation";
        }
    }
}



[HttpPost]
[Route("api/update-post-data")]
public String UpdatePostData(string postId, String userId,string name, String content)
{
    using (var context = new Entities())
    {
        int postDataId = Int16.Parse(postId);
        int PostUserId = Int16.Parse(userId);
        Detail ctUser = context.Details.Where(usr => usr.Id == PostUserId).FirstOrDefault();
        if (ctUser != null)
        {

            ComposePost isDataExist = context.ComposePosts.Where(a => a.Id == postDataId && a.User_id == PostUserId).FirstOrDefault();
            if (isDataExist !=null) {
                        
                isDataExist.Content = content;
                isDataExist.Name = name;
                isDataExist.Created_date = DateTime.Now;

                Db.Entry(isDataExist).State = EntityState.Modified;
                Db.SaveChanges();
                return "Post Update Successfully";
            }
            else{
                return "Error in Post Update Successfully";
            }
        }
        else
        {
            return "UnSuccessful Operation";
        }
    }
}


}
}