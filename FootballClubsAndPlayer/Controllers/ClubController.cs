﻿using Business.Services;
using DataAccess;
using Entities.Models;
using System;
using System.Collections.Generic;
using Utilities.Helper;

namespace FootballClubsAndPlayer.Controllers
{
    public class ClubController
    {

        ClubService clubService = new ClubService();

        #region METHODS

        /// <summary>
        /// Daxil edilen melumatlar daxilinde klub yaradib siyahiya add edir
        /// </summary>
        public void CreatClub()
        {
            Notifications.Print(ConsoleColor.Yellow, "Please enter the Club name:");
            string clubName = Chek.StrNull();
            Notifications.Print(ConsoleColor.Yellow, "Please enter the Club Maximum player size:");
            int Msize = Chek.NumTryPars();
            Notifications.Print(ConsoleColor.Yellow, "Please enter the Country Id :");
            int Countid = Chek.NumTryPars();
            Club club = new Club()
            {
                ClubName = clubName,
                MaxPSize = Msize,
                CreatTeam = DateTime.Today,
                FootballPlayers = new List<FootballPlayer>()

            };

            clubService.Create(club);
            Notifications.Print(ConsoleColor.Yellow, $"{club.ClubName} created");
        }
            /// <summary>
            /// Eger her hansi bir ad versem o adda olan klublari cixardacaq ekrana
            /// </summary>
        public void GetClubs()
        {
            if (DataContext.Clubs.Count == 0)
            {
                Console.Beep();
                Notifications.Print(ConsoleColor.Red, "Firstly you have to create a club");
            }
            else
            {
                foreach (var item in clubService.Get())
                {
                    Notifications.Print(ConsoleColor.Blue, $"ID: {item.ID}--CLUB NAME: {item.ClubName}--TEAM CREATED: {item.CreatTeam.Month}/{item.CreatTeam.Day}/{item.CreatTeam.Year}");
                    foreach (var i in item.FootballPlayers)
                    {

                        Notifications.Print(ConsoleColor.White, $"PlayerID: {i.ID}--NAME:{i.PlayerName}--SURNAME: {i.PlayerSurname}--AGE: {i.Age}");
                    }
                }
            }

        }

        /// <summary>
        /// clubun verilmis yeni infolarini bazadaki idli clubun infolarina deyisir
        /// </summary>
        public void UpdateClub()
        {
            if (DataContext.Clubs.Count == 0)
            {
                Console.Beep();
                Notifications.Print(ConsoleColor.Red, "Firstly you have to create a club");
            }
            else
            {
                Notifications.Print(ConsoleColor.Red, "All Clubs");
                GetClubs();

                Notifications.Print(ConsoleColor.Yellow, "Change the Clup ID for Update");
                int idchek = Chek.NumTryPars();

                Notifications.Print(ConsoleColor.Yellow, "Enter the new Name to Clup for Update");
                string newname=Chek.StrNull();

                Notifications.Print(ConsoleColor.Yellow, "Enter the new size to Clup for Update");
                int newsize = Chek.NumTryPars();


                Club clubnew = new Club()
                {
                    ClubName = newname,
                    MaxPSize = newsize,

                };
                clubService.Update(clubnew, idchek);
            }
        }

        /// <summary>
        /// verilen idli klubu bazadan silir
        /// </summary>
        public void DeleteClub()
        {
            if (DataContext.Clubs.Count == 0)
            {
                Console.Beep();
                Notifications.Print(ConsoleColor.Red, "Firstly you have to create a club");
            }
            else
            {
                Notifications.Print(ConsoleColor.Red, "All Clubs");
                GetClubs();

                Notifications.Print(ConsoleColor.Yellow, "Change the Clup ID for Delete");
                int idchek = Chek.NumTryPars();
                clubService.Delete(idchek);
            }
        }

        /// <summary>
        /// secilen id li kluba verilmis melubatlar daxilinde oyuncular add etmek ucundu
        /// </summary>
        public void AddPlayerToClub()
        {
            if (DataContext.Clubs.Count == 0)
            {
                Console.Beep();
                Notifications.Print(ConsoleColor.Red, "Firstly You have to creat a club");
            }
            else
            {
                GetClubs();

                Notifications.Print(ConsoleColor.Yellow, "Please enter the Player name:");
                string playerName = Chek.StrNull();

                Notifications.Print(ConsoleColor.Yellow, "Please enter the Player surname:");
                string playerSurname = Chek.StrNull();
            A:
                Notifications.Print(ConsoleColor.Yellow, "Please enter the Player Age:");
                int age = Chek.NumTryPars();
                if (age == 0)
                {
                    Notifications.Print(ConsoleColor.Red, "Age doesn't be zero or minus!! please enter the correctly");
                    goto A;
                }
            Play:
                Notifications.Print(ConsoleColor.Yellow, "Please enter the Player Number:");
                int playNum = Chek.NumTryPars();
                foreach (var item in clubService.Get())
                {
                    foreach (var plays in item.FootballPlayers)
                    {
                        if (playNum == plays.PlayerNum || playNum == 0)
                        {
                            Notifications.Print(ConsoleColor.Red, "players of the same number will not or deosn't be zero!! please enter correctly");
                            Notifications.Print(ConsoleColor.Yellow, $"{plays.PlayerNum}");
                            goto Play;
                        }
                    }   
                }

                Notifications.Print(ConsoleColor.Yellow, "Please enter the Player Club Id:");
                int clubId = Chek.NumTryPars();

                FootballPlayer player = new FootballPlayer()
                {
                    PlayerName = playerName,
                    PlayerSurname = playerSurname,
                    ClubId = clubId,
                    Age = age,
                    PlayerNum = playNum

                };

                clubService.AddPlayerToClub(player);
            }
        }

        /// <summary>
        /// daxil edilmis id li oyuncunu secilmis klubdan yeni secilen kluba transfer edir
        /// </summary>
        public void TransferPlayerToAnyClub()
        {
            if (DataContext.Clubs.Count == 0)
            {
                Console.Beep();
                Notifications.Print(ConsoleColor.Red, "Firstly You have to creat a club");
            }
            else
            {
                Notifications.Print(ConsoleColor.Green, "All Clubs and Players");
                GetClubs();

                Notifications.Print(ConsoleColor.Red, "Change Id player for the transfer");
                int playerid = Chek.NumTryPars();

                Notifications.Print(ConsoleColor.Red, "Change Old Clubd id for the transfer");
                int oldclubid = Chek.NumTryPars();

                Notifications.Print(ConsoleColor.Red, "Change New Club Id for the transfer");
                int newclubid = Chek.NumTryPars();

                clubService.TransferPlayer(playerid, oldclubid, newclubid);
            }

        }
        #endregion
    }
}
