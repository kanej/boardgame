using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoardGame;

namespace BoardGameTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void a_new_game_has_the_player_in_the_bottom_left_hand_corner()
        {
            var game = new Game();

            var expectedBoard = @"00000000
00000000
00000000
00000000
00000000
00000000
00000000
X0000000";

            Assert.AreEqual(expectedBoard, game.ToString());
        }

        [TestMethod]
        public void moving_right_shifts_the_marker()
        {
            var game = new Game();
            game.Move("RIGHT");

            var expectedBoard = @"00000000
00000000
00000000
00000000
00000000
00000000
00000000
0X000000";

            Assert.AreEqual(expectedBoard, game.ToString());
        }

        [TestMethod]
        public void moving_up_shifts_the_marker()
        {
            var game = new Game();
            game.Move("UP");

            var expectedBoard = @"00000000
00000000
00000000
00000000
00000000
00000000
X0000000
00000000";

            Assert.AreEqual(expectedBoard, game.ToString());
        }
    }
}
