using System.Linq;

namespace BoardGame
{
    public class Board
    {
        private int Height { get; set; }
        private int Width { get; set; }

        private char[] Markers { get; set; }

        public Board(int height, int width)
        {
            this.Height = height;
            this.Width = width;
            this.Markers = new char[this.Height * this.Width];

            for (var i = 0; i < this.Markers.Length; i++)
            {
                this.Markers[i] = '0';
            }
        }

        public void SetMarker(int x, int y, char marker)
        {
            var index = (this.Width * y) + x;
            this.Markers[index] = marker;
        }

        public override string ToString()
        {
            var rows = this.Markers.Partition(this.Width);

            var combinedRows = rows.Reverse().Select(row => { return string.Concat(row); });

            return string.Join("\r\n", combinedRows);
        }
    }
}
