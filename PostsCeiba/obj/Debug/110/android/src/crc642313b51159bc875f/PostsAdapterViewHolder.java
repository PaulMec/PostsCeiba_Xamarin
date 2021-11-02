package crc642313b51159bc875f;


public class PostsAdapterViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("PostsCeiba.Adapters.PostsAdapterViewHolder, PostsCeiba", PostsAdapterViewHolder.class, __md_methods);
	}


	public PostsAdapterViewHolder ()
	{
		super ();
		if (getClass () == PostsAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("PostsCeiba.Adapters.PostsAdapterViewHolder, PostsCeiba", "", this, new java.lang.Object[] {  });
	}

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
