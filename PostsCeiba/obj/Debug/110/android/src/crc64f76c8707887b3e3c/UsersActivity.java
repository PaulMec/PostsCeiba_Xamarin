package crc64f76c8707887b3e3c;


public class UsersActivity
	extends androidx.appcompat.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("PostsCeiba.Activity.UsersActivity, PostsCeiba", UsersActivity.class, __md_methods);
	}


	public UsersActivity ()
	{
		super ();
		if (getClass () == UsersActivity.class)
			mono.android.TypeManager.Activate ("PostsCeiba.Activity.UsersActivity, PostsCeiba", "", this, new java.lang.Object[] {  });
	}


	public UsersActivity (int p0)
	{
		super (p0);
		if (getClass () == UsersActivity.class)
			mono.android.TypeManager.Activate ("PostsCeiba.Activity.UsersActivity, PostsCeiba", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
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
