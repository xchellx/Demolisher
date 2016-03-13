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
			updateIndexLabel(lblIndexNum, mObject.Parent);
			updateIndexLabel(lblChildNum, mObject.Child);
			updateIndexLabel(lblNextNum, mObject.Next);
			updateIndexLabel(lblPrevNum, mObject.Prev);

			chkFlag1.Checked = mObject.hasFlag(demoObjectFlags.UNK01);
			chkFlag2.Checked = mObject.hasFlag(demoObjectFlags.UNK02);
			chkFlag3.Checked = mObject.hasFlag(demoObjectFlags.FOURTHWALL);
			chkFlag4.Checked = mObject.hasFlag(demoObjectFlags.TRANSPARENT);
			chkFlag5.Checked = mObject.hasFlag(demoObjectFlags.UNK10);
			chkFlag6.Checked = mObject.hasFlag(demoObjectFlags.UNK20);
			chkFlag7.Checked = mObject.hasFlag(demoObjectFlags.FULLBRIGHT);
			chkFlag8.Checked = mObject.hasFlag(demoObjectFlags.CEILING);
		}
		void updateIndexLabel(Label label, int index) {
			if (index >= 0) {
				label.Enabled = true;
				label.Text = String.Format("#{0:D3}", index);
			}
			else {
				label.Enabled = false;
				label.Text = "None";
			}
		}

		public demoObject setObject(demoObject obj) {
			var old = mObject;
			mObject = obj;
			update();
			return old;
		}
	}
}
