using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        [SerializeField] private Transform blockPrefab;
        [SerializeField] private Transform blockHolder;

        [SerializeField] private TMPro.TextMeshProUGUI livesText;

        private Transform currentBlock = null;
        private Rigidbody2D currentRigidbody;
        private Vector2 blockStartPosition = new Vector2(0f, 4f);

        private float blockSpeed = 8f;
        private float blockSpeedIncrement = 0.3f;
        private int blockDirection = 1; // 1 for right, -1 for left
        private float xLimit = 5;
        private float timeBetweenRounds = 0.5f;

        private int startingLives = 3;
        private int livesRemaining;
        private bool playing = true;

        void Start()
        {
                livesRemaining = startingLives;
                livesText.text = "Lives: " + livesRemaining;
                SpawnNewBlock();
        }

        private void SpawnNewBlock()
        {
                float randomX = Random.Range(-xLimit, xLimit);
                Vector2 spawnPosition = new Vector2(randomX, blockStartPosition.y);
                currentBlock = Instantiate(blockPrefab, blockHolder);
                currentBlock.position = spawnPosition;
                currentRigidbody = currentBlock.GetComponent<Rigidbody2D>();
                blockSpeed += blockSpeedIncrement;
        }

        private IEnumerator DelayedSpawn()
        {
                yield return new WaitForSeconds(timeBetweenRounds);
                SpawnNewBlock();
        }


        void Update()
        {
                if (currentBlock && playing)
                {
                        float moveAmount = Time.deltaTime * blockSpeed * blockDirection;
                        currentBlock.position += new Vector3(moveAmount, 0, 0);
                        if (Mathf.Abs(currentBlock.position.x) > xLimit)
                        {
                                currentBlock.position = new Vector3(blockDirection * xLimit, currentBlock.position.y, 0);
                                blockDirection *= -1;
                        }
                }
                if (Input.GetKeyDown(KeyCode.Space) && currentBlock && playing)
                {
                        currentBlock = null;
                        currentRigidbody.simulated = true;
                        StartCoroutine(DelayedSpawn());
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                }
        }

        public void RemoveLife()
        {
                livesRemaining = Mathf.Max(livesRemaining - 1, 0);
                livesText.text = "Lives: " + livesRemaining;
                if (livesRemaining <= 0)
                {
                        playing = false;
                        livesText.text = "Game Over! Press R to Restart";
                }
        }


}
