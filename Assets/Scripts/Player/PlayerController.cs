using ButchersGames;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float sensitivity;
        [SerializeField] private float maxRotationSpeed;
        [SerializeField] private float trackWidth = 3f;


        private Pathway _pathway;

        private Transform _origin;
        [SerializeField]private Transform _rotativeObj;
        [SerializeField] private float maxRotationAngle = 30f;
        private float _currentHorizontalInput;
        private float _rotation;

        

        private void Awake()
        {
            _origin = transform.parent;
        }

        public void ResetToStart(Level level)
        {
            _pathway = level.transform.GetComponentInChildren<Pathway>();
            _origin.position = level.PlayerSpawnPoint;
            _origin.rotation = Quaternion.identity;
            _rotation = 0;
            transform.localPosition = Vector3.zero;
            _pathway.Reset();
            _rotativeObj.localRotation = Quaternion.identity; // Сброс поворота объекта
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // Получаем первое касание

                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    OnMove(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _currentHorizontalInput = 0; // Сбрасываем ввод при окончании касания
                }
            }
            else
            {
                _currentHorizontalInput = 0; // Сбрасываем, если касаний нет
            }
        }

        private void FixedUpdate()
        {
            
            if (!GameManager.Instance.GameIsOn) return;
            var targetRotation = _pathway.GetTargetRotation(_origin.position);
            var targetDeltaRotation = targetRotation - _rotation;
            if (Mathf.Abs(targetDeltaRotation) < maxRotationSpeed) _rotation = targetRotation;
            else _rotation += Mathf.Sign(targetDeltaRotation) * maxRotationSpeed;
            _origin.rotation = Quaternion.AngleAxis(_rotation, Vector3.up);
            _origin.Translate(Time.fixedDeltaTime * speed * Vector3.forward);
            float rotationAngle = _currentHorizontalInput * 10 * maxRotationAngle;
            _rotativeObj.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
        }

        private void OnMove(Vector2 touchPosition)
        {
            var gameManager = GameManager.Instance;

            // Определяем направление движения по горизонтали
            float screenWidth = Screen.width;
            float touchXNormalized = (touchPosition.x / screenWidth) - 0.5f; // Нормализуем от -0.5 до 0.5

            _currentHorizontalInput = touchXNormalized;


            if (gameManager.WaitingForMove && _currentHorizontalInput > 0)
            {
                gameManager.StartGame();
            }

            if (!gameManager.GameIsOn) return;


            var targetDeltaPosition = _currentHorizontalInput * sensitivity;
            var targetPosition = transform.localPosition.x + targetDeltaPosition;

            targetPosition = Mathf.Clamp(targetPosition, -trackWidth / 2f, trackWidth / 2f); // Ограничиваем позицию

            transform.localPosition = new Vector3(targetPosition, transform.localPosition.y, transform.localPosition.z); // Изменяем только X


        }
    }
}
