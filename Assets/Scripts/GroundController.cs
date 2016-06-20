using UnityEngine;
using System.Collections;
using UnityEditor;
using SeafoodEditorHelper;
using System;

[CustomEditor(typeof(PlayerStateMachine))]
public class PlayerStateMachineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerStateMachine myScript = (PlayerStateMachine)target;
        if (GUILayout.Button("Reset StateMachine"))
        {
            myScript.GotoState("GroundState");
            EditorGUIUtility.ExitGUI();
        }
    }
}

[CustomEditor(typeof(MountainBuilder))]
public class MountainBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(1) });

        MountainBuilder myScript = (MountainBuilder)target;
        if (GUILayout.Button("Build Basic"))
        {
            myScript.BuildBasic();
            EditorGUIUtility.ExitGUI();
        }
    }
}

[CustomEditor(typeof(MountainPartBuilder))]
public class MountainPartBuilderEditor : Editor
{
    static int selectedNeighbor = 0;
    static int selectedpart = 0;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(1) });

        string[] neighborOptions = Enum.GetNames(typeof(MountainNeighborType));
        selectedNeighbor = EditorGUILayout.Popup("NeighborType", selectedNeighbor, neighborOptions);

        string[] partOptions = Enum.GetNames(typeof(MountainPartType));
        selectedpart = EditorGUILayout.Popup("PartType", selectedpart, partOptions);

        if (GUILayout.Button("Build Neighbor"))
        {
            MountainPartBuilder myScript = (MountainPartBuilder)target;
            myScript.BuildNeighbor((MountainNeighborType)selectedNeighbor, (MountainPartType)selectedpart);
            EditorGUIUtility.ExitGUI();
        }
    }
}

//public class BasePlayerController : MonoBehaviour
//{
//    public IAvatar TheAvatar { get; set; }
//}

//public class GroundController : BasePlayerController
//{
//    public float Speed = 5.0f;

//    public Old2GroundState State { set; get; }

//    private void Update()
//    {
//        if (Input.GetButtonDown("Jump"))
//            State.Jump();
//    }

//    private void FixedUpdate()
//    {
//        var direction = Input.GetAxisRaw("Horizontal");
//        var x = Mathf.Abs(Input.GetAxis("Horizontal"));
//        TheAvatar.anim.SetFloat("GroundSpeed", x);
//        TheAvatar.rb2d.velocity = new Vector2(direction * x * Speed, TheAvatar.rb2d.velocity.y);

//        if (direction != 0)
//            TheAvatar.SetDirection(direction > 0);
//    }
//}

//public class JumpController : BasePlayerController
//{
//    public float Speed = 5.0f;
//    public float JumpForce = 1.0f;

//    private void FixedUpdate()
//    {
//        var direction = Input.GetAxisRaw("Horizontal");
//        var x = Mathf.Abs(Input.GetAxis("Horizontal"));
//        TheAvatar.rb2d.velocity = new Vector2(direction * x * Speed, TheAvatar.rb2d.velocity.y);

//        if (direction != 0)
//            TheAvatar.SetDirection(direction > 0);

//        var y = Mathf.Abs(Input.GetAxis("Jump"));
//        TheAvatar.rb2d.AddForce(new Vector2(0, JumpForce));
//    }
//}

//public class OldBaseController : MonoBehaviour
//{
//    protected Animator animator;
//    protected Rigidbody2D rb2d;
//    protected bool facingRight = true;
//    protected OldPlayer player;

//    protected void Awake()
//    {
//        animator = GetComponent<Animator>();
//        rb2d = GetComponent<Rigidbody2D>();
//        player = GetComponent<OldPlayer>();
//    }

//    protected void SetDirection(bool isRight)
//    {
//        if (facingRight != isRight)
//        {
//            facingRight = isRight;

//            var theScale = transform.localScale;
//            theScale.x *= -1;
//            transform.localScale = theScale;
//        }
//    }
//}

//public class OldGroundController : OldBaseController
//{
//    public float Speed = 5.0f;

//    private void Update() { }

//    private void FixedUpdate()
//    {
//        var direction = Input.GetAxisRaw("Horizontal");
//        var x = Mathf.Abs(Input.GetAxis("Horizontal"));
//        animator.SetFloat("GroundSpeed", x);
//        rb2d.velocity = new Vector2(direction * x * Speed, rb2d.velocity.y);

//        if (direction != 0)
//            SetDirection(direction > 0);

//        if (Input.GetButtonDown("Jump"))
//            player.Jump();
//    }

//    private void OnDisable()
//    {
//        animator.SetFloat("GroundSpeed", 0);
//    }
//}


//public class OldJumpController : OldBaseController
//{
//    public float Speed = 5.0f;

//    private void OnEnable()
//    {
//        animator.SetTrigger("Jump");
//    }

//    private void FixedUpdate()
//    {
//        var direction = Input.GetAxisRaw("Horizontal");
//        var x = Mathf.Abs(Input.GetAxis("Horizontal"));
//        rb2d.velocity = new Vector2(direction * x * Speed, rb2d.velocity.y);

//        if (direction != 0)
//            SetDirection(direction > 0);
//    }
//}
