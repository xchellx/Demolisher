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
			grpHierarchy.Visible = !hidden;
			grpFlags.Visible = !hidden;
		}
		void update() {
			if (mObject == null) {
				setHidden(true);
				return;
			}
			setHidden(false);
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

		public demoObject setObject(demoObject obj) {
			var old = mObject;
			mObject = obj;
			update();
			return old;
		}
	}
}
