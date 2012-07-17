using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using RemoteMon_Lib;

namespace RemoteMon
{
    public sealed class DataGridViewGraphColumn : DataGridViewColumn
    {
        public DataGridViewGraphColumn()
        {
            //Assembly a = Assembly.GetExecutingAssembly();
            //String[] names = a.GetManifestResourceNames();
            //Stream s = a.GetManifestResourceStream("RemoteMon.Properties.Resources.graphbg");
            
            //BitMap = b;//new Bitmap(s);
            CellTemplate = new DataGridViewGraphCell();
            ReadOnly = true;
        }
    }

    public class DataGridViewGraphCell : DataGridViewTextBoxCell
    {
        private static Bitmap _bitMap;
        private static readonly Pen _greenPen = new Pen(Color.LightGreen, 1f);
        private static readonly Pen _redPen = new Pen(Color.Red, 1f);
        private static readonly Pen _orangePen = new Pen(Color.Orange, 1f);
        private static readonly Pen _lightGrayPen = new Pen(Color.LightGray, 1f);

        private Single _panicValue;
        private Single _warningValue;
        private Boolean _lessThan = false;
        private Single _currentMax;
        private Single _currentMin;

        private Single MaxValue
        {
            get
            {
                Single cmv = CurrentMaxValue;
                Single ncmv = cmv * 1.10f;
                return Single.IsInfinity(ncmv) ? cmv : ncmv;
            }
        }
        private Single MinValue
        {
            get
            {
                //Single cmv = CurrentMinValue;
                Single ncmv = CurrentMinValue * .9f;
                return ncmv;
            }
        }
        private Single CurrentMaxValue
        {
            get
            {
                Single max = 0;
                foreach (Single d in _values.Values)
                {
                    if (d > max)
                        max = d;
                }
                _currentMax = max == 0 ? Single.MaxValue : max;
                return _currentMax;
            }
        }
        private Single CurrentMinValue
        {
            get
            {
                Single min = Single.MaxValue;
                foreach (Single d in _values.Values)
                {
                    if (d < min)
                        min = d;
                }
                _currentMin = min == Single.MaxValue ? 0 : min;
                return _currentMin;
            }
        }

        private readonly Dictionary<Int32, Single> _values;
        private Int32 _currentIndex = 0;
        private Single CurrentValue
        {
            get { return _values[_currentIndex == -1 ? 0 : _currentIndex]; }
            set
            {
                _currentIndex++;

                if (_currentIndex < 10)
                {
                    _values[_currentIndex] = value;
                }
                else
                {
                    _currentIndex = 0;
                    _values[_currentIndex] = value;
                }
            }
        }
        private IEnumerable<Single> GetValuesInOrder()
        {
            List<Int32> ints = new List<Int32>(10);
            Int32 i = _currentIndex + 1 < 10 ? _currentIndex + 1 : 0;
            
            for(Int32 x = 0; x < 10; x++)
            {
                ints.Add(i);
                if (i < 9)
                {
                    i++;
                    if (i == _currentIndex)
                    {
                        ints.Add(i);
                        break;
                    }
                }
                else
                {
                    i = 0;
                    if (i == _currentIndex)
                    {
                        ints.Add(i);
                        break;
                    }
                }
            }
            foreach (Int32 num in ints)
            {
                yield return _values[num];
            }
        }

        public DataGridViewGraphCell()
        {
            _values = new Dictionary<Int32, Single>(10);
            for (int i = 0; i < 10; i++)
                _values.Add(i, 0f);

            //_lightGrayPen = new Pen(Color.LightGray, 1f);
            //_greenPen = new Pen(Color.LightGreen, 1f);//.FromKnownColor(KnownColor.Green));
            //_redPen = new Pen(Color.Red, 1f);
            //_orangePen = new Pen(Color.Orange, 1f);

            if (_bitMap == null)
            {
                try
                {
                    ResourceManager rm = new ResourceManager("RemoteMon.Properties.Resources",
                                                                Assembly.GetExecutingAssembly());
                    _bitMap = (Bitmap)rm.GetObject("graphbg");
                }
                catch (Exception ex)
                {
                    Exception exbmp = new Exception("Unable to access graphbg resource.", ex);

                    Logger.Instance.LogException(this.GetType(), exbmp);

                    MessageBox.Show("The GridView was unable to initialize.  please check the log for more details.",
                                    "Error", MessageBoxButtons.OK);
                    Application.Exit();
                }
            }
        }



        protected override void Paint(Graphics graphics, Rectangle clipBounds,
            Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState,
            object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            Single val;
            //NOTE: if it can parse a number, then it'll graph it, otherwise just put the text.
            if (value != null && Single.TryParse(value.ToString(), out val))
            {
                //NOTE: image is a 79x25
                //CurrentValue = val;// SetValue(val);

                this.ToolTipText = "Current Value: " + CurrentValue + 
                                   ", Current Min: " + _currentMin +
                                   ", Current Max: " + _currentMax;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graphics.DrawImage(_bitMap, cellBounds);
                graphics.DrawLine(_lightGrayPen, cellBounds.Location.X,
                                  cellBounds.Location.Y + _bitMap.Height - 1,
                                  cellBounds.Location.X + cellBounds.Width,
                                  cellBounds.Location.Y + _bitMap.Height - 1);
                //graphics.DrawString("potatos" + rowIndex.ToString(), new Font(new FontFamily(System.Drawing.Text.GenericFontFamilies.Serif), 12f), new SolidBrush(Color.Green), cellBounds);
                PointPen[] points = GetPoints(cellBounds, _bitMap.Size);
                //graphics.DrawLine(DrawLines(_greenPen, points));
                for(int x = 0; x < points.Length; x++)
                {
                    if(x-1 >= 0)// points.Length)
                        graphics.DrawLine(points[x].Pen, points[x-1].Point, points[x].Point); //points[x].Point, points[x + 1].Point);
                }
                //graphics.Dispose();
            }
            else
            {
                this.ToolTipText = "";
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText,
                           cellStyle, advancedBorderStyle, paintParts);
            }
        }

        private PointPen[] GetPoints(Rectangle cellBounds, Size bitmapSize)
        {
            List<PointPen> points = new List<PointPen>(10);
            Int32 i = -1;
            Single min = MinValue;
            Single max = MaxValue;
            foreach (Single f in GetValuesInOrder()) //F is the value
            {
                Single x = cellBounds.Location.X + (++i * ((cellBounds.Width - 2f) / 9f));
                //Single range = 1 / (max - min);
                //Single val = (f - min) / range;
                Single tmp = ((f - min)/(max - min));//25
                Single y = cellBounds.Location.Y + (cellBounds.Height - (tmp * cellBounds.Height)) - 2;// -(cellBounds.Size.Height - 1);

                Pen p = _lessThan
                            ? (f > _warningValue ? (f > _panicValue ? _greenPen : _orangePen) : _redPen)
                            : (f < _panicValue ? (f < _warningValue ? _greenPen : _orangePen) : _redPen);
                points.Add(new PointPen(p, new PointF(x, y)));
            }

            return points.ToArray();
        }


        public void SetValue(Object value)
        {
            //CurrentValue = value;
            Single val;
            CurrentValue = Single.TryParse(value.ToString(), out val) ? val : 0f;
            //if (val < _currentMin)
            //    _currentMin = val;
            //else if (val > _currentMax)
            //    _currentMax = val;
        }
        public void LessThan(Boolean lessThan)
        {
            _lessThan = lessThan;
        }
        public void SetPanic(Single panicValue)
        {
            _panicValue = panicValue;
        }
        public void SetWarning(Single warningValue)
        {
            _warningValue = warningValue;
        }

        private class PointPen
        {
            public Pen Pen;
            public PointF Point;
            public PointPen(Pen pen, PointF point)
            {
                Pen = pen;
                Point = point;
            }
        }

        public new static void Dispose()
        {
            _bitMap.Dispose();
            _lightGrayPen.Dispose();
            _greenPen.Dispose();
            _orangePen.Dispose();
            _redPen.Dispose();
        }
        //protected override void Dispose(bool disposing)
        //{
        //    //_bitMap.Dispose();
        //    //_lightGrayPen.Dispose();
        //    //_greenPen.Dispose();
        //    //_orangePen.Dispose();
        //    //_redPen.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}