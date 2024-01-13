using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Animator animator;
    private bool isOpen;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        Lean.Touch.LeanTouch.OnFingerTap += HandleFingerTap;
        isOpen = true;
    }

    private void OnDisable()
    {
        Lean.Touch.LeanTouch.OnFingerTap -= HandleFingerTap;
    }

    private void HandleFingerTap(Lean.Touch.LeanFinger finger)
    {
        Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);

        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
            return;

        if (hit.collider.gameObject == gameObject)
        {
            isOpen = !isOpen;
            animator.SetBool("Open_Anim", isOpen);
            //Create a cube
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = hit.point;
        }

        if (hit.collider.CompareTag("Floor"))
            transform.position = hit.point;
    }
}
