using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Arookas.Windows
{
	// Token: 0x0200008F RID: 143
	public class Hotkey : IMessageFilter
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000E869 File Offset: 0x0000CA69
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0000E871 File Offset: 0x0000CA71
		public Keys KeyCode
		{
			get
			{
				return this.keyCode;
			}
			set
			{
				if (this.keyCode != value)
				{
					this.keyCode = value;
					this.Update();
				}
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000E889 File Offset: 0x0000CA89
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000E891 File Offset: 0x0000CA91
		public HotkeyModifiers ModifierKeys
		{
			get
			{
				return this.modifierKeys;
			}
			set
			{
				if (this.modifierKeys != value)
				{
					this.modifierKeys = value;
					this.Update();
				}
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000E8A9 File Offset: 0x0000CAA9
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000E8C1 File Offset: 0x0000CAC1
		public bool AltModifier
		{
			get
			{
				return this.modifierKeys.HasFlag(HotkeyModifiers.Alt);
			}
			set
			{
				this.SetModifierKey(HotkeyModifiers.Alt, value);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000E8CB File Offset: 0x0000CACB
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000E8E3 File Offset: 0x0000CAE3
		public bool ControlModifier
		{
			get
			{
				return this.modifierKeys.HasFlag(HotkeyModifiers.Control);
			}
			set
			{
				this.SetModifierKey(HotkeyModifiers.Control, value);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000E8ED File Offset: 0x0000CAED
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000E905 File Offset: 0x0000CB05
		public bool ShiftModifier
		{
			get
			{
				return this.modifierKeys.HasFlag(HotkeyModifiers.Shift);
			}
			set
			{
				this.SetModifierKey(HotkeyModifiers.Shift, value);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000E90F File Offset: 0x0000CB0F
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0000E927 File Offset: 0x0000CB27
		public bool WindowsModifier
		{
			get
			{
				return this.modifierKeys.HasFlag(HotkeyModifiers.Windows);
			}
			set
			{
				this.SetModifierKey(HotkeyModifiers.Windows, value);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000E931 File Offset: 0x0000CB31
		public bool IsEmpty
		{
			get
			{
				return this.keyCode == Keys.None;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000E93C File Offset: 0x0000CB3C
		public bool IsRegistered
		{
			get
			{
				return this.registeredControl != null;
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000485 RID: 1157 RVA: 0x0000E94C File Offset: 0x0000CB4C
		// (remove) Token: 0x06000486 RID: 1158 RVA: 0x0000E984 File Offset: 0x0000CB84
		public event HandledEventHandler Pressed;

		// Token: 0x06000487 RID: 1159 RVA: 0x0000E9B9 File Offset: 0x0000CBB9
		public Hotkey() : this(Keys.None, HotkeyModifiers.None)
		{
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000E9C3 File Offset: 0x0000CBC3
		public Hotkey(Keys keyCode, HotkeyModifiers modifierKeys)
		{
			this.keyCode = keyCode;
			this.modifierKeys = modifierKeys;
			Application.AddMessageFilter(this);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000E9DF File Offset: 0x0000CBDF
		public Hotkey(Control registerControl, Keys keyCode, HotkeyModifiers modifierKeys) : this(keyCode, modifierKeys)
		{
			this.Register(registerControl);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		~Hotkey()
		{
			if (this.IsRegistered)
			{
				this.Unregister();
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000EA24 File Offset: 0x0000CC24
		public bool CanRegister(Control registerControl)
		{
			bool result;
			try
			{
				this.Register(registerControl);
				result = true;
			}
			catch
			{
				result = false;
			}
			finally
			{
				this.Unregister();
			}
			return result;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000EA68 File Offset: 0x0000CC68
		public Hotkey Clone()
		{
			return new Hotkey(this.keyCode, this.modifierKeys);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		private bool OnPressed()
		{
			HandledEventArgs handledEventArgs = new HandledEventArgs(false);
			if (this.Pressed != null)
			{
				this.Pressed(this, handledEventArgs);
			}
			return handledEventArgs.Handled;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000EAAC File Offset: 0x0000CCAC
		public bool PreFilterMessage(ref Message message)
		{
			return (long)message.Msg == 786L && (this.IsRegistered && message.WParam.ToInt32() == this.hotkeyID) && this.OnPressed();
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		public void Register(Control registerControl)
		{
			if (this.IsRegistered)
			{
				throw new InvalidOperationException("You cannot register a Hotkey that is already registered.");
			}
			if (this.IsEmpty)
			{
				throw new InvalidOperationException("You cannot register an empty Hotkey.");
			}
			this.hotkeyID = Hotkey.currentHotkeyID;
			Hotkey.currentHotkeyID = (Hotkey.currentHotkeyID + 1) % 49151;
			if (Hotkey.RegisterHotKey(registerControl.Handle, this.hotkeyID, (uint)this.modifierKeys, this.keyCode) != 0)
			{
				this.registeredControl = registerControl;
				return;
			}
			if ((long)Marshal.GetLastWin32Error() == 1409L)
			{
				throw new Exception("The Hotkey has already been registered.");
			}
			throw new Win32Exception();
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000EB88 File Offset: 0x0000CD88
		private void SetModifierKey(HotkeyModifiers modifierKey, bool value)
		{
			HotkeyModifiers hotkeyModifiers = this.modifierKeys;
			if (value)
			{
				this.modifierKeys |= modifierKey;
			}
			else
			{
				this.modifierKeys &= ~modifierKey;
			}
			if (this.modifierKeys != hotkeyModifiers)
			{
				this.Update();
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		public override string ToString()
		{
			if (this.IsEmpty)
			{
				return "(none)";
			}
			StringBuilder stringBuilder = new StringBuilder(10);
			if (this.ShiftModifier)
			{
				stringBuilder.Append("Shift+");
			}
			if (this.ControlModifier)
			{
				stringBuilder.Append("Control+");
			}
			if (this.AltModifier)
			{
				stringBuilder.Append("Alt+");
			}
			if (this.WindowsModifier)
			{
				stringBuilder.Append("Windows+");
			}
			string text = Enum.GetName(typeof(Keys), this.keyCode);
			switch (this.keyCode)
			{
			case Keys.D0:
			case Keys.D1:
			case Keys.D2:
			case Keys.D3:
			case Keys.D4:
			case Keys.D5:
			case Keys.D6:
			case Keys.D7:
			case Keys.D8:
			case Keys.D9:
				text = text[1].ToString();
				break;
			}
			stringBuilder.Append(text);
			return stringBuilder.ToString();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
		public void Unregister()
		{
			if (!this.IsRegistered)
			{
				throw new NotSupportedException("You cannot unregister a Hotkey that is not registered");
			}
			if (!this.registeredControl.IsDisposed && Hotkey.UnregisterHotKey(this.registeredControl.Handle, this.hotkeyID) == 0)
			{
				throw new Win32Exception();
			}
			this.registeredControl = null;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		private void Update()
		{
			if (!this.IsRegistered)
			{
				return;
			}
			Control registerControl = this.registeredControl;
			this.Unregister();
			this.Register(registerControl);
		}

		// Token: 0x06000494 RID: 1172
		[DllImport("user32.dll", SetLastError = true)]
		private static extern int RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);

		// Token: 0x06000495 RID: 1173
		[DllImport("user32.dll", SetLastError = true)]
		private static extern int UnregisterHotKey(IntPtr hWnd, int id);

		// Token: 0x040001EC RID: 492
		private const int maximumHotkeyID = 49151;

		// Token: 0x040001ED RID: 493
		private const uint WM_HOTKEY = 786U;

		// Token: 0x040001EE RID: 494
		private const uint ERROR_HOTKEY_ALREADY_REGISTERED = 1409U;

		// Token: 0x040001EF RID: 495
		private Keys keyCode;

		// Token: 0x040001F0 RID: 496
		private HotkeyModifiers modifierKeys;

		// Token: 0x040001F1 RID: 497
		private Control registeredControl;

		// Token: 0x040001F2 RID: 498
		private int hotkeyID;

		// Token: 0x040001F4 RID: 500
		private static int currentHotkeyID;
	}
}
