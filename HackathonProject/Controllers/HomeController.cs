using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HackathonProject.Models;

namespace HackathonProject.Controllers
{
    public class HomeController : Controller
    {
        public static HackathonEntities1 entities = new HackathonEntities1();

        public static List<SelectListItem> CategoryList = entities.AllHouses.Select(c => new SelectListItem()
        {
            Value = c.Category,
            Text = c.Category
        }).ToList();

        public static List<SelectListItem> ResultLigList = entities.Result_LIGs.Select(c => new SelectListItem()
        {
            Value = c.MobileNumber,
            Text = c.MobileNumber
        }).ToList();
        public static List<SelectListItem> ResultHigList = entities.Result_HIGs.Select(c => new SelectListItem()
        {
            Value = c.MobileNumber,
            Text = c.MobileNumber
        }).ToList();
        public static List<SelectListItem> ResultMigList = entities.Result_MIGs.Select(c => new SelectListItem()
        {
            Value = c.MobileNumber,
            Text = c.MobileNumber
        }).ToList();

        public static List<SelectListItem> RequestLigList = entities.Request_LIGs.Select(c => new SelectListItem()
        {
            Value = c.MobileNumber,
            Text = c.MobileNumber
        }).ToList();
        public static List<SelectListItem> RequestHigList = entities.Request_HIGs.Select(c => new SelectListItem()
        {
            Value = c.MobileNumber,
            Text = c.MobileNumber
        }).ToList();
        public static List<SelectListItem> RequestMigList = entities.Request_MIGs.Select(c => new SelectListItem()
        {
            Value = c.MobileNumber,
            Text = c.MobileNumber
        }).ToList();
        private static Random _random = new Random();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Category = CategoryList;
            ViewBag.Message = null;
            return View();
        }
        [HttpPost]
        public ActionResult Register(AllUser user)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Category = CategoryList;
                ViewBag.Message = null;
                entities.AllUsers.Add(user);
                entities.SaveChanges();
                TempData["Mobile"] = user.MobileNumber;
                return RedirectToAction("BookHouse");
            }
            else
            {
                return View();
            }            
        }

        [HttpGet]
        public ActionResult BookHouse()
        {
            ViewBag.Category = CategoryList;
            ViewBag.Message = null;
            return View();
        }
        [HttpPost]
        public ActionResult BookHouse(AllHous allHous)
        {
            if (allHous.Category == null)
            {
                ViewBag.Category = CategoryList;
                ViewBag.Message = "Select All Field(s) !";
                return View("BookHouse");
            }
            else
            {
                ViewBag.Message = null;
                string mobileNumber = TempData["Mobile"].ToString();
                if(allHous.Category == "LIG")
                {
                    Request_LIG request_LIG = new Request_LIG()
                    {
                        Category = allHous.Category,
                        MobileNumber = mobileNumber
                    };

                    var maxApplicants = entities.AllHouses.Where(a => a.Category == "LIG").Select(a => a.MaxApplicants).First();
                    if (maxApplicants == RequestLigList.Count)
                    {
                        var firstvalue = entities.Request_LIGs.Where(a => a.Category == "LIG").Select(a => a.Id).First();
                        List<int> seed = new List<int>(Enumerable.Range(firstvalue, maxApplicants));
                        var inventory = entities.AllHouses.Where(a => a.Category == "LIG").Select(a => a.Inventory).First();

                        for (int i = 0; i < inventory; i++)
                        {
                            int initial = _random.Next(seed.Count);
                            int result = seed[initial];
                            seed.RemoveAt(initial);
                            foreach (var item in entities.Request_LIGs.ToList())
                            {
                                if(result == item.Id)
                                {
                                    Result_LIG _LIG = new Result_LIG()
                                    {
                                        MobileNumber = item.MobileNumber
                                    };
                                    entities.Result_LIGs.Add(_LIG);
                                    entities.SaveChanges();
                                   
                                }
                            }                            
                        }
                        TempData["LigMessage"] = "LIG Maximum application limit Reached!";
                    }
                    else
                    {
                        TempData["LigMessage"] = "LIG request sent Successfully!";
                        entities.Request_LIGs.Add(request_LIG);
                        entities.SaveChanges();
                    }                    
                }
                else if (allHous.Category == "HIG")
                {
                    Request_HIG request_HIG = new Request_HIG()
                    {
                        Category = allHous.Category,
                        MobileNumber = mobileNumber
                    };
                    var maxApplicants = entities.AllHouses.Where(a => a.Category == "HIG").Select(a => a.MaxApplicants).First();
                    if (maxApplicants == RequestHigList.Count)
                    {
                        var firstvalue = entities.Request_HIGs.Where(a => a.Category == "HIG").Select(a => a.Id).First();
                        List<int> seed = new List<int>(Enumerable.Range(firstvalue, maxApplicants));
                        var inventory = entities.AllHouses.Where(a => a.Category == "HIG").Select(a => a.Inventory).First();
                        for (int i = 0; i < inventory; i++)
                        {
                            int initial = _random.Next(seed.Count);
                            int result = seed[initial];
                            seed.RemoveAt(initial);
                            foreach (var item in entities.Request_HIGs.ToList())
                            {
                                if (result == item.Id)
                                {
                                    Result_HIG _HIG = new Result_HIG()
                                    {
                                        MobileNumber = item.MobileNumber
                                    };
                                    entities.Result_HIGs.Add(_HIG);
                                    entities.SaveChanges();
                                    
                                }
                            }
                        }
                        TempData["HigMessage"] = "HIG Maximum application limit Reached!";
                    }
                    else
                    {
                        TempData["HigMessage"] = "HIG request sent Successfully!";
                        entities.Request_HIGs.Add(request_HIG);
                        entities.SaveChanges();
                    }                   
                }
                if (allHous.Category == "MIG")
                {
                    Request_MIG request_MIG = new Request_MIG()
                    {
                        Category = allHous.Category,
                        MobileNumber = mobileNumber
                    };
                    var maxApplicants = entities.AllHouses.Where(a => a.Category == "MIG").Select(a => a.MaxApplicants).First();
                    if (maxApplicants == RequestMigList.Count)
                    {
                        var firstvalue = entities.Request_MIGs.Where(a => a.Category == "MIG").Select(a => a.Id).First();
                        List<int> seed = new List<int>(Enumerable.Range(firstvalue, maxApplicants));
                        var inventory = entities.AllHouses.Where(a => a.Category == "MIG").Select(a => a.Inventory).First();
                        for (int i = 0; i < inventory; i++)
                        {
                            int initial = _random.Next(seed.Count);
                            int result = seed[initial];
                            seed.RemoveAt(initial);
                            foreach (var item in entities.Request_MIGs.ToList())
                            {
                                if (result == item.Id)
                                {
                                    Result_MIG _MIG = new Result_MIG()
                                    {
                                        MobileNumber = item.MobileNumber
                                    };
                                    entities.Result_MIGs.Add(_MIG);
                                    entities.SaveChanges();
                                    
                                }
                            }
                        }
                        TempData["MigMessage"] = "MIG Maximum application limit Reached!";
                    }
                    else
                    {
                        TempData["MigMessage"] = "MIG request sent Successfully!";
                        entities.Request_MIGs.Add(request_MIG);
                        entities.SaveChanges();
                    }
                }
                return RedirectToAction("ApplicationStatus");
            }
        }
        public ActionResult ApplicationStatus()
        {
            return View();
        }

        public ActionResult LotteryResults()
        {
            ViewBag.LigListCount = ResultLigList.Count;
            ViewBag.HigListCount = ResultHigList.Count;
            ViewBag.MigListCount = ResultMigList.Count;
            ViewBag.LigList = ResultLigList;
            ViewBag.HigList = ResultHigList;
            ViewBag.MigList = ResultMigList;
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}