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
        public void moving_left_shifts_the_marker()
        {
            var game = new Game();
            game.Move("RIGHT");
            game.Move("LEFT");

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

        [TestMethod]
        public void moving_down_shifts_the_marker()
        {
            var game = new Game();
            game.Move("UP");
            game.Move("DOWN");

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
        public void an_unknown_move_returns_a_failure_result()
        {
            var game = new Game();
            var result = game.Move("UNKNOWN");

            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Unknown Move - UNKNOWN", result.Message);
        }

        [TestMethod]
        public void moving_to_the_right_of_the_rightmost_column_returns_a_failure_result()
        {
            var game = new Game();

            game.Move("RIGHT");
            game.Move("RIGHT");
            game.Move("RIGHT");
            game.Move("RIGHT");
            game.Move("RIGHT");
            game.Move("RIGHT");
            game.Move("RIGHT");
            game.Move("RIGHT");
            var result = game.Move("RIGHT");

            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Already on the rightmost column", result.Message);
        }

        [TestMethod]
        public void moving_to_the_left_of_the_leftmost_column_returns_a_failure_result()
        {
            var game = new Game();

            var result = game.Move("LEFT");

            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Already on the leftmost column", result.Message);
        }

        [TestMethod]
        public void moving_above_the_top_row_returns_a_failure_result()
        {
            var game = new Game();

            game.Move("UP");
            game.Move("UP");
            game.Move("UP");
            game.Move("UP");
            game.Move("UP");
            game.Move("UP");
            game.Move("UP");
            game.Move("UP");

            var result = game.Move("UP");

            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Already on the top row", result.Message);
        }

        [TestMethod]
        public void moving_bellow_the_bottom_row_returns_a_failure_result()
        {
            var game = new Game();

            var result = game.Move("DOWN");

            Assert.AreEqual("Failure", result.Status);
            Assert.AreEqual("Already on the bottom row", result.Message);
        }
    }
}
