using arookas.Math;
using OpenTK;
using System;
using System.Windows.Forms;

namespace arookas {
	partial class demoObjectPropsControl : UserControl {
		demoObject mObject;

		public demoObjectPropsControl() {
			InitializeComponent();
			update();
		}

		void setHidden(bool hidden) {
			tblMain.Visible = !hidden;
		}

		void update() {
			if (mObject == null) {
				setHidden(true);
				return;
			}
			setHidden(false);

			updateVectorLabel(lblPosition, lblPositionNum, mObject.Position, Vector3.Zero);
			updateVectorLabel(lblRotation, lblRotationNum, mObject.Rotation, Vector3.Zero);
			updateVectorLabel(lblScale, lblScaleNum, mObject.Scale, Vector3.One);

			updateIndexLabel(lblIndex, lblIndexNum, mObject.Parent);
			updateIndexLabel(lblChild, lblChildNum, mObject.Child);
			updateIndexLabel(lblNext, lblNextNum, mObject.Next);
			updateIndexLabel(lblPrev, lblPrevNum, mObject.Prev);

			updateFlagLabel(chkFlag1, demoObjectFlags.UNK01);
			updateFlagLabel(chkFlag2, demoObjectFlags.UNK02);
			updateFlagLabel(chkFlag3, demoObjectFlags.FOURTHWALL);
			updateFlagLabel(chkFlag4, demoObjectFlags.TRANSPARENT);
			updateFlagLabel(chkFlag5, demoObjectFlags.UNK10);
			updateFlagLabel(chkFlag6, demoObjectFlags.UNK20);
			updateFlagLabel(chkFlag7, demoObjectFlags.FULLBRIGHT);
			updateFlagLabel(chkFlag8, demoObjectFlags.CEILING);
		}
		void updateVectorLabel(Label name, Label num, Vector3 vec, Vector3 def) {
			float x, y, z;
			var isdef = roundVector(vec, def, out x, out y, out z);
			name.Enabled = !isdef;
			num.Enabled = !isdef;
			num.Text = String.Format("({0}, {1}, {2})", x, y, z);
		}
		void updateIndexLabel(Label name, Label num, int index) {
			if (index >= 0) {
				name.Enabled = true;
				num.Enabled = true;
				num.Text = String.Format("#{0:D3}", index);
			}
			else {
				name.Enabled = false;
				num.Enabled = false;
				num.Text = "None";
			}
		}
		void updateFlagLabel(CheckBox check, demoObjectFlags flag) {
			var on = mObject.hasFlag(flag);
			check.Checked = on;
			check.Enabled = on;
		}

		static bool roundVector(Vector3 vec, Vector3 def, out float x, out float y, out float z) {
			x = (float)System.Math.Round(vec.X, 2, MidpointRounding.AwayFromZero);
			y = (float)System.Math.Round(vec.Y, 2, MidpointRounding.AwayFromZero);
			z = (float)System.Math.Round(vec.Z, 2, MidpointRounding.AwayFromZero);
			return approximately(vec, def);
		}
		static bool approximately(Vector3 a, Vector3 b) {
			return a.X.Approximately(b.X) && a.Y.Approximately(b.Y) && a.Z.Approximately(b.Z);
		}

		public demoObject setObject(demoObject obj) {
			var old = mObject;
			mObject = obj;
			update();
			return old;
		}
	}
}
