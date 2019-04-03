using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Diagnostics;
using Nancy.Hosting.Self;
using Nancy.Routing;
using Nancy.TinyIoc;
using Newtonsoft.Json;

namespace S3
{
    public class InformationUpdate
    {
        public Player Player1;
        public Player Player2;
        public string round;
        public string tournamentName;
        public string caster;
        public string streamer;
        public string iconStyle;
        public string matchType;
    }
    public class CrewUpdate
    {
        public string Crew1;
        public string Crew2;
        public int Crew1Stocks;
        public int Crew2Stocks;

        public string round;
        public string tournamentName;
        public string caster;
        public string streamer;
        public string iconStyle;
        public string matchType = "Crew Battle";

        public Crewmember A1;
        public Crewmember A2;
        public Crewmember A3;
        public Crewmember A4;
        public Crewmember A5;

        public Crewmember B1;
        public Crewmember B2;
        public Crewmember B3;
        public Crewmember B4;
        public Crewmember B5;
    }
    public class BracketUpdate
    {
        public Bracket WSA;
        public Bracket WSB;
        public Bracket WF;
        public Bracket GF;
        public Bracket BR;
        public Bracket LF;
        public Bracket LSF;
        public Bracket LQA;
        public Bracket LQB;
        public Bracket L8A;
        public Bracket L8B;

        public Bracket DWSA;
        public Bracket DWSB;
        public Bracket DWF;
        public Bracket DGF;
        public Bracket DBR;
        public Bracket DLF;
        public Bracket DLSF;
        public Bracket DLQA;
        public Bracket DLQB;
    }

    public class Player
    {
        public string name;
        public Character character;
        public Sponsor sponsor;
        public int score;
        public Flag flag;
        public string color;
    }
    public class Crewmember
    {
        public string name;
        public Character character;
    }

    public class Bracket
    {
        public string Player1;
        public string Player2;
        public int Player1score;
        public int Player2score;
    }

    public class Server : NancyModule
    {
        public Server()
        {
            Get["/getCurrentValues"] = parameters =>
            {
                InformationUpdate update = Globals.CurrentInformationUpdate;
                string data = JsonConvert.SerializeObject(update);
                Response response = (Response)data;
                response.ContentType = "application/json";
                return response;
            };
            Get["/getCrewValues"] = parameters =>
            {
                CrewUpdate update = Globals.CurrentCrewUpdate;
                string data = JsonConvert.SerializeObject(update);
                Response response = (Response)data;
                response.ContentType = "application/json";
                return response;
            };
            Get["/getBracketValues"] = parameters =>
            {
                BracketUpdate update = Globals.bracketInfo;
                string data = JsonConvert.SerializeObject(update);
                Response response = (Response)data;
                response.ContentType = "application/json";
                return response;
            };
            Get["/scoreboard"] = paramaters =>
            {
                return Response.AsFile("Content/index.html", "text/html");
            };
        }

        public static NancyHost Run(Uri uri)
        {
            try
            {
                HostConfiguration config = new HostConfiguration();
                config.UrlReservations.CreateAutomatically = true;
                NancyHost host = new NancyHost(config, uri);
                
                host.Start();
                return host;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }
    }

    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            // your customization goes here
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {

            //CORS Enable
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");

            });
        }
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("assets", @"Content/html/*")
            );
        }
    }
}
