using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExperienceBag : MonoBehaviour
{
        public static ExperienceBag Instance;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        [Header("Prefabs")]
        [SerializeField] private GameObject experiencePrefab;
        
        [Header("Bullet Lists")]
        public List<GameObject> experienceList;

        private void Start()
        {
            CreateExperience(20);
        }

        public GameObject FindExperience()
        {
            for (var b = 0; b < experienceList.Count; b++)
            {
                if (experienceList[b].activeSelf == false)
                {
                    return experienceList[b];
                }

                if (b == experienceList.Count - 1)
                {
                    CreateExperience(10);
                }
            }

            return  FindExperience();
        }
        
        private void CreateExperience(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var experience = Instantiate(experiencePrefab, transform);
                experienceList.Add(experience);

                experience.name = experience.name.ToString() + experienceList.Count;
                
                experience.transform.parent = null;
                
                experience.SetActive(false);
            }
        }

        public void SpawnExperience(Vector3 position)
        {
            FindExperience().GetComponent<ExperienceOrb>().Spawn(position);
            StartCoroutine(CollectAllExperience());
        }

        public IEnumerator CollectAllExperience()
        {
            yield return new WaitForSeconds(10.0f);
            foreach (var exp in experienceList.Where(exp => exp.activeSelf))
            {
                exp.GetComponent<ExperienceOrb>().GoToPlayer();
            }
            
            StopAllCoroutines();
        }
    }
