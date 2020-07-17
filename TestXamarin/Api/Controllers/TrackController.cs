using System.Collections.Generic;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Track> Get()
        {
            return new List<Track> { 
                new Track 
                {
                    Artist = "Guns and Roses",
                    Id = "1",
                    Name = "Illusion",
                    Year = "1992",
                    ImageUrl="https://cdn.shopify.com/s/files/1/0882/5118/products/Guns-n-Roses-Use-Your-Illusion-I-Vinyl-2-LP-1707302_1024x1024.jpg?v=1467391724"
                },
                new Track
                {
                    Artist = "Nirvana",
                    Id = "2",
                    Name = "Utero",
                    Year = "1993",
                    ImageUrl = "https://www.mautorland.com/wp-content/uploads/2018/06/DAE415B8-C5B7-401A-96A1-E6B770EC4EAD.jpeg"
                },
                new Track
                {
                    Artist = "Green Day",
                    Id = "3",
                    Name = "Dookey",
                    Year = "1994",
                    ImageUrl = "https://i.pinimg.com/originals/23/01/fb/2301fb0901062fe74be1c06c703ff24c.jpg"
                },
                new Track
                {
                    Artist = "Bon Jovi",
                    Id = "4",
                    Name = "These days",
                    Year = "1995",
                    ImageUrl = "https://musiclife.cl/2694-medium_default/bon-jovi-these-days.jpg"
                },
                new Track
                {
                    Artist = "ColdPlay",
                    Id = "5",
                    Name = "Parachutes",
                    Year = "2000",
                    ImageUrl = "https://www.impericon.com/360x520x85/media/catalog/product/0/7/0724352778324-8005-0600px-001_lg.jpg"
                },
                new Track
                {
                    Artist = "ColdPlay",
                    Id = "6",
                    Name = "X&Y",
                    Year = "2005",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/2/28/Coldplay_X%26Y.svg/220px-Coldplay_X%26Y.svg.png"
                },
                new Track
                {
                    Artist = "My Chemical Romance",
                    Id = "7",
                    Name = "The Black Parade",
                    Year = "2006",
                    ImageUrl = "https://img.discogs.com/kmT1byGO0eSg3WZ8Gd2qJsfFxLc=/fit-in/300x300/filters:strip_icc():format(jpeg):mode_rgb():quality(40)/discogs-images/R-815226-1348500314-3481.jpeg.jpg"
                },
                new Track
                {
                    Artist = "Simple Plan",
                    Id = "8",
                    Name = "Still Not Getting Any",
                    Year = "2004",
                    ImageUrl = "https://img.discogs.com/NrvsilUedSPOPjnsZPyR0mWGoW8=/fit-in/300x300/filters:strip_icc():format(jpeg):mode_rgb():quality(40)/discogs-images/R-10507774-1506516210-8184.jpeg.jpg"
                },
                new Track
                {
                    Artist = "Aerosmith",
                    Id = "9",
                    Name = "Nine lives",
                    Year = "1997",
                    ImageUrl = "https://img.discogs.com/8lbxmq7i0u2eUUz_q1jeUOooCGg=/fit-in/300x300/filters:strip_icc():format(jpeg):mode_rgb():quality(40)/discogs-images/R-423821-1466877566-7859.jpeg.jpg"
                },
                new Track
                {
                    Artist = "Queen",
                    Id = "10",
                    Name = "A Night At the opera",
                    Year = "1975",
                    ImageUrl = "https://lastfm.freetls.fastly.net/i/u/770x0/d36a1f8075da4df6bb835b2f7eac1547.webp#d36a1f8075da4df6bb835b2f7eac1547"
                },
            };
        }
    }
}
