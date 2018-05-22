package md57f3133b84bd47da701afafa2c6483dff;


public class AccountSettingsView
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("DATABASE1111111.AccountSettingsView, DATABASE1111111", AccountSettingsView.class, __md_methods);
	}


	public AccountSettingsView ()
	{
		super ();
		if (getClass () == AccountSettingsView.class)
			mono.android.TypeManager.Activate ("DATABASE1111111.AccountSettingsView, DATABASE1111111", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
