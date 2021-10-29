using Laba11._2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web;

namespace Laba11._2.Controllers
{
    public class PerformerSong
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SongTitleSong
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class WApiController : ApiController
    {
        private Lab2DBEntities db = new Lab2DBEntities();

        [HttpGet]
        [ActionName("GetPerformer")]
        public ICollection<PerformerSong> GetPerformer()
        {
            var pf = (from performer in db.Performers select performer).ToList();
            Collection<PerformerSong> perf = new Collection<PerformerSong>();
            foreach (Performer p in pf)
            {
                perf.Add(new PerformerSong { Id = p.IdPerformer, Name = p.NamePerformer });
            }
            return perf;
        }

        [HttpGet]
        [ActionName("GetSongTitle")]
        public ICollection<SongTitleSong> GetSongTitle(int id)
        {
            var songt = (from song in db.Songs
                         join songTitle in db.SongTitles on song.IdSongTitle equals songTitle.IdSongTitle
                         where song.IdPerformer == id
                         select songTitle).ToList();
            Collection<SongTitleSong> sts = new Collection<SongTitleSong>();
            foreach (Models.SongTitle t in songt)
            {
                sts.Add(new SongTitleSong { Id = t.IdSongTitle, Name = t.SongTitle1 });
            }
            return sts;
        }

        [HttpPost]
        [ActionName("CreateSongTitle")]
        public HttpResponseMessage CreateSongTitle(SongTitle st)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                db.SongTitles.Add(st);
                db.SaveChanges();
                response.Content = new StringContent("{IdSongTitle: " + st.IdSongTitle + " ,IdSong: " + st.IdSong + " ,SongTitle: " + st.SongTitle1 + "}", Encoding.UTF8, "application/json");
            }
            catch (Exception e)
            {
                response.Content = new StringContent("{Error: " + e.Message + "}", Encoding.UTF8, "application/json");
            }
            return response;
        }

        [HttpPost]
        [ActionName("CreatePerformer")]
        public HttpResponseMessage CreatePerformer(Performer p)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                db.Performers.Add(p);
                db.SaveChanges();
                response.Content = new StringContent("{IdPerformer: " + p.IdPerformer + " ,NamePerformer: " + p.NamePerformer + " ,NumberOfSongs: " + p.NumberOfSongs + "}", Encoding.UTF8, "application/json");
            }
            catch (Exception e)
            {
                response.Content = new StringContent("{Error: " + e.Message + "}", Encoding.UTF8, "application/json");
            }
            return response;
        }

        [HttpPost]
        [ActionName("UpdatePerformer")]
        public HttpResponseMessage UpdatePerformer(Performer p)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            var br = (from performer in db.Performers where performer.IdPerformer == p.IdPerformer select performer).First();

            try
            {
                db.Performers.Remove(br);
                db.Performers.Add(p);
                db.SaveChanges();
                response.Content = new StringContent("{IdPerformer: " + p.IdPerformer + " ,NamePerformer: " + p.NamePerformer + " ,NumberOfSongs: " + p.NumberOfSongs + "}", Encoding.UTF8, "application/json");
            }
            catch (Exception e)
            {
                response.Content = new StringContent("{Error: " + e.Message + "}", Encoding.UTF8, "application/json");
            }
            return response;
        }

        [HttpPost]
        [ActionName("DeletePerformer")]
        public HttpResponseMessage DeletePerformer(Performer p)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            var br = (from performer in db.Performers where performer.IdPerformer == p.IdPerformer select performer).First();

            try
            {
                db.Performers.Remove(br);
                db.SaveChanges();
                response.Content = new StringContent("{IdPerformer: " + br.IdPerformer + " ,NamePerformer: " + br.NamePerformer + " ,NumberOfSongs: " + br.NumberOfSongs + "}", Encoding.UTF8, "application/json");
            }
            catch (Exception e)
            {
                response.Content = new StringContent("{Error: " + e.Message + "}", Encoding.UTF8, "application/json");
            }
            return response;
        }

        
    }
}
