﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using X_O_Game.Properties;

namespace X_O_Game
{
    public partial class XOGame : Form
    {

        public XOGame()
        {
            InitializeComponent();
        }
        //************** STRUCT UND ENUM **********************************************************
        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;

        }

        stGameStatus GameStatus;

        enum enPlayer
        {
            Player1,
            Player2
        }

        enPlayer PlayerTurn = enPlayer.Player1;

        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        }

        //************************************************************************
        //******************* FUNCTION PROGRESS *****************************************************
        void EndGame()
        {
            lb_Player.Text = "Game Over";

            switch (GameStatus.Winner)
            {

                case enWinner.Player1:
                    lb_WinnerName.Text = "Player 1";
                    break;

                case enWinner.Player2:
                    lb_WinnerName.Text = "Player 2";
                    break;

                default:
                    lb_WinnerName.Text = "Draw";
                    break;


            }

            MessageBox.Show("GameOver", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public bool CheckValues(Button btn1, Button btn2, Button btn3)
        {

            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if (btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }

            }

            GameStatus.GameOver = false;

            return false;

        }

        public void CheckWinner()
        {

            //checked rows
            //check Row1
            if (CheckValues(button1, button2, button3))
                return;

            //check Row2
            if (CheckValues(button4, button5, button6))
                return;

            //check Row3
            if (CheckValues(button7, button8, button9))
                return;

            //checked cols
            //check col1
            if (CheckValues(button1, button4, button7))
                return;

            //check col2
            if (CheckValues(button2, button5, button8))
                return;

            //check col3
            if (CheckValues(button3, button6, button9))
                return;

            //check Diagonal

            //check Diagonal1
            if (CheckValues(button1, button5, button9))
                return;

            //check Diagonal2
            if (CheckValues(button3, button5, button7))
                return;

        }

        public void ChangeImage(Button btn)
        {

            if (btn.Tag.ToString() =="?") 
            {
                
                switch (PlayerTurn)
                {

                    case enPlayer.Player1:

                        btn.Image = Resources.X;
                        PlayerTurn = enPlayer.Player2;
                        lb_Player.Text = "Player 2";
                        GameStatus.PlayCount++;
                        btn.Tag = "X";                
                        CheckWinner();

                        break;

                    case enPlayer.Player2:

                        btn.Image = Resources.O;
                        PlayerTurn = enPlayer.Player1;
                        lb_Player.Text = "Player 1";
                        GameStatus.PlayCount++;
                        btn.Tag = "O";
                        CheckWinner();

                        break;

                }


            }
        }
        //************************************************************************
        //************** NAVI FUNCTION **********************************************************

        private void RestButton(Button btn)
        {

            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;

        }

        private void RestartGame()
        {

            RestButton(button1);
            RestButton(button2);
            RestButton(button3);
            RestButton(button4);
            RestButton(button5);
            RestButton(button6);
            RestButton(button7);
            RestButton(button8);
            RestButton(button9);

            PlayerTurn = enPlayer.Player1;
            lb_Player.Text = "Player 1";
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.GameInProgress;
            lb_Winner.Text = "In Progress";

        }

        private void btn_RestartGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.FromArgb(255, 255, 255, 255);
            Pen whitePen = new Pen(white);
            whitePen.Width = 15;
            //whitePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            //draw Horizental lines
            e.Graphics.DrawLine(whitePen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(whitePen, 400, 460, 1050, 460);

            //draw Vertical lines
            e.Graphics.DrawLine(whitePen, 610, 140, 610, 620);
            e.Graphics.DrawLine(whitePen, 840, 140, 840, 620);
        }

        //************************************************************************
        //************ BUTTON CLICK ************************************************************
        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }

    }
}
