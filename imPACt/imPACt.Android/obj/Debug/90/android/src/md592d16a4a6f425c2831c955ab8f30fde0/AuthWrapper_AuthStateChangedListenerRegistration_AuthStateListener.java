package md592d16a4a6f425c2831c955ab8f30fde0;


public class AuthWrapper_AuthStateChangedListenerRegistration_AuthStateListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.firebase.auth.FirebaseAuth.AuthStateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAuthStateChanged:(Lcom/google/firebase/auth/FirebaseAuth;)V:GetOnAuthStateChanged_Lcom_google_firebase_auth_FirebaseAuth_Handler:Firebase.Auth.FirebaseAuth/IAuthStateListenerInvoker, Xamarin.Firebase.Auth\n" +
			"";
		mono.android.Runtime.register ("Plugin.FirebaseAuth.AuthWrapper+AuthStateChangedListenerRegistration+AuthStateListener, Plugin.FirebaseAuth", AuthWrapper_AuthStateChangedListenerRegistration_AuthStateListener.class, __md_methods);
	}


	public AuthWrapper_AuthStateChangedListenerRegistration_AuthStateListener ()
	{
		super ();
		if (getClass () == AuthWrapper_AuthStateChangedListenerRegistration_AuthStateListener.class)
			mono.android.TypeManager.Activate ("Plugin.FirebaseAuth.AuthWrapper+AuthStateChangedListenerRegistration+AuthStateListener, Plugin.FirebaseAuth", "", this, new java.lang.Object[] {  });
	}


	public void onAuthStateChanged (com.google.firebase.auth.FirebaseAuth p0)
	{
		n_onAuthStateChanged (p0);
	}

	private native void n_onAuthStateChanged (com.google.firebase.auth.FirebaseAuth p0);

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
