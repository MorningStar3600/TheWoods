﻿using System;
using System.Threading;

namespace Projet
{
    class Navigation
    {
        /// <summary>Allow to navigate throug the sliders</summary>
        /// <param name="ActivateSlider">Array with the slider used</param>
        /// <returns>Array with the new slider in use and the old one</returns>
        public static int[] NavigationManager(int[] ActivateSlider)
        {
            bool result = false;
            int reDraw = 1;
            Program.SetOnlyRedrawMap(2);
            do
            {
                Thread.Sleep(100);
                
                Console.SetCursorPosition(0, 0);
                ActivateSlider[1] = ActivateSlider[0];
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        for (int i = 0; i < Slider.GetSliderList().Length; i++)
                        {
                            if (i == ActivateSlider[0] && ActivateSlider[0] != 10)
                            {
                                if (Slider.GetSliderListByID(i).GetLastSelect() > 0)
                                {
                                    Slider.GetSliderList()[i].SetLastSelect(Slider.GetSliderListByID(i).GetLastSelect() - 1);
                                    if (ActivateSlider[0] == 3)
                                    {
                                        reDraw = 2;
                                    }
                                }
                            }else if(i == ActivateSlider[0] && ActivateSlider[0] == 10)
                            {
                                if (Program.GetTurn() > 0)
                                {                
                                    Program.DecrTurn();
                                    reDraw = 2;
                                }
                            }

                        }
                        result = true;
                        break;
                    case ConsoleKey.RightArrow:
                        for (int i = 0; i < Slider.GetSliderList().Length; i++)
                        {
                            if (i == ActivateSlider[0] && ActivateSlider[0] != 10)
                            {
                                if (Slider.GetSliderListByID(i).GetLastSelect() < Slider.GetSliderListByID(i).GetValue().Length - 1)
                                {
                                    Slider.GetSliderList()[i].SetLastSelect(Slider.GetSliderListByID(i).GetLastSelect() + 1);
                                    if (ActivateSlider[0] == 3)
                                    {
                                        reDraw = 2;
                                    }
                                }
                            }
                            else if (i == ActivateSlider[0] && ActivateSlider[0] == 10)
                            {
                                if (Program.GetTurn() < Program.GetMaps().Count-1)
                                {
                                    Program.IncrTurn();
                                    reDraw = 2;
                                }
                            }
                        }
                        result = true;
                        break;
                    case ConsoleKey.UpArrow:
                        if (ActivateSlider[0] > 0)
                        {
                            ActivateSlider[1] = ActivateSlider[0];
                            ActivateSlider[0]--;
                            int activePosition = Slider.GetSliderListByID(ActivateSlider[0]).GetPosition();
                            for (int i = 1; i < Slider.GetSliderList().Length; i++)
                            {
                                if (activePosition < 2)
                                {
                                    Slider.GetSliderListByID(i).SetPosition(Slider.GetSliderListByID(i).GetPosition() + 4);
                                }
                            }
                        }
                        result = true;
                        break;
                    case ConsoleKey.DownArrow:
                        if (ActivateSlider[0] < Slider.GetSliderList().Length - 1)
                        {
                            ActivateSlider[1] = ActivateSlider[0];
                            ActivateSlider[0]++;
                            int activePosition = Slider.GetSliderListByID(ActivateSlider[0]).GetPosition();
                            for (int i = 1; i < Slider.GetSliderList().Length; i++)
                            {
                                if (activePosition > Console.WindowHeight - 2)
                                {
                                    Slider.GetSliderListByID(i).SetPosition(Slider.GetSliderListByID(i).GetPosition() - 4);
                                }
                            }
                        }
                        result = true;
                        break;
                    case ConsoleKey.Enter:
                        WoodGenerator.GenerateWoods();
                        reDraw = 3;
                        result = true;
                        break;
                    case ConsoleKey.Escape:
                        ActivateSlider[0] = -1;
                        result = true;
                        break;
                    case ConsoleKey.Spacebar:
                        Program.SetTurn(Program.GetMaps().Count - 1);
                        Program.SaveMaps(Program.GetMaps(), Program.GetMap(), false);
                        Program.PassTour(Program.GetMap());
                        reDraw = 2;
                        result = true;
                        break;
                    case ConsoleKey.F:
                        Program.SetTurn(Program.GetMaps().Count - 1);
                        Program.InitiateFire(Program.GetMap());
                        Program.SaveMaps(Program.GetMaps(), Program.GetMap(), false);
                        reDraw = 2;
                        result = true;
                        break;
                }
           
                
                Program.SetOnlyRedrawMap(reDraw);
                

            } while (!result);
            WoodGenerator.SetTreeProportion((Slider.GetSliderListByID(5).GetLastSelect()+1)*10);
            WoodGenerator.SetWaterProportion((Slider.GetSliderListByID(6).GetLastSelect()+1)*10);
            WoodGenerator.SetLeaveProportion((Slider.GetSliderListByID(7).GetLastSelect() + 1) * 10);
            WoodGenerator.SetRockProportion((Slider.GetSliderListByID(8).GetLastSelect() + 1) * 10);
            WoodGenerator.SetGrassProportion((Slider.GetSliderListByID(9).GetLastSelect() + 1) * 10);
            return ActivateSlider;
        }

    }
}
