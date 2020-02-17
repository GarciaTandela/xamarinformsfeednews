package crc644db0af6ef61d292f;


public class CountingRequestBody
	extends com.squareup.okhttp.RequestBody
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_contentType:()Lcom/squareup/okhttp/MediaType;:GetContentTypeHandler\n" +
			"n_contentLength:()J:GetContentLengthHandler\n" +
			"n_writeTo:(Lokio/BufferedSink;)V:GetWriteTo_Lokio_BufferedSink_Handler\n" +
			"";
		mono.android.Runtime.register ("Plugin.FileUploader.CountingRequestBody, Plugin.FileUploader", CountingRequestBody.class, __md_methods);
	}


	public CountingRequestBody ()
	{
		super ();
		if (getClass () == CountingRequestBody.class)
			mono.android.TypeManager.Activate ("Plugin.FileUploader.CountingRequestBody, Plugin.FileUploader", "", this, new java.lang.Object[] {  });
	}


	public com.squareup.okhttp.MediaType contentType ()
	{
		return n_contentType ();
	}

	private native com.squareup.okhttp.MediaType n_contentType ();


	public long contentLength ()
	{
		return n_contentLength ();
	}

	private native long n_contentLength ();


	public void writeTo (okio.BufferedSink p0)
	{
		n_writeTo (p0);
	}

	private native void n_writeTo (okio.BufferedSink p0);

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
