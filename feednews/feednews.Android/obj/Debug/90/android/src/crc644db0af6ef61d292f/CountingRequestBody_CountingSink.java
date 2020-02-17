package crc644db0af6ef61d292f;


public class CountingRequestBody_CountingSink
	extends okio.ForwardingSink
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_write:(Lokio/Buffer;J)V:GetWrite_Lokio_Buffer_JHandler\n" +
			"";
		mono.android.Runtime.register ("Plugin.FileUploader.CountingRequestBody+CountingSink, Plugin.FileUploader", CountingRequestBody_CountingSink.class, __md_methods);
	}


	public CountingRequestBody_CountingSink (okio.Sink p0)
	{
		super (p0);
		if (getClass () == CountingRequestBody_CountingSink.class)
			mono.android.TypeManager.Activate ("Plugin.FileUploader.CountingRequestBody+CountingSink, Plugin.FileUploader", "OkHttp.Okio.ISink, OkHttp", this, new java.lang.Object[] { p0 });
	}


	public void write (okio.Buffer p0, long p1)
	{
		n_write (p0, p1);
	}

	private native void n_write (okio.Buffer p0, long p1);

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
