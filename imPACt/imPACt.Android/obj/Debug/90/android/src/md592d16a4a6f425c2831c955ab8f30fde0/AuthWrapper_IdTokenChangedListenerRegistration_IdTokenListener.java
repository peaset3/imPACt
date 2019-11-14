package md592d16a4a6f425c2831c955ab8f30fde0;


public class AuthWrapper_IdTokenChangedListenerRegistration_IdTokenListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.firebase.auth.FirebaseAuth.IdTokenListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onIdTokenChanged:(Lcom/google/firebase/auth/FirebaseAuth;)V:GetOnIdTokenChanged_Lcom_google_firebase_auth_FirebaseAuth_Handler:Firebase.Auth.FirebaseAuth/IIdTokenListenerInvoker, Xamarin.Firebase.Auth\n" +
			"";
		mono.android.Runtime.register ("Plugin.FirebaseAuth.AuthWrapper+IdTokenChangedListenerRegistration+IdTokenListener, Plugin.FirebaseAuth", AuthWrapper_IdTokenChangedListenerRegistration_IdTokenListener.class, __md_methods);
	}


	public AuthWrapper_IdTokenChangedListenerRegistration_IdTokenListener ()
	{
		super ();
		if (getClass () == AuthWrapper_IdTokenChangedListenerRegistration_IdTokenListener.class)
			mono.android.TypeManager.Activate ("Plugin.FirebaseAuth.AuthWrapper+IdTokenChangedListenerRegistration+IdTokenListener, Plugin.FirebaseAuth", "", this, new java.lang.Object[] {  });
	}


	public void onIdTokenChanged (com.google.firebase.auth.FirebaseAuth p0)
	{
		n_onIdTokenChanged (p0);
	}

	private native void n_onIdTokenChanged (com.google.firebase.auth.FirebaseAuth p0);

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
