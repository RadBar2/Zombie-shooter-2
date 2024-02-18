using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Radoslav : Enemy
{
    public GameObject death;

    private Animator _animator;

    private NavMeshAgent _agent;

    public GameObject Player;

    public float AttackDistance = 10.0f;

    public float FollowDistance = 20.0f;

    [Range(0.0f, 1.0f)]
    public float AttackProbability = 0.5f;

    [Range(0.0f, 1.0f)]
    public float HitAccuracy = 0.5f;

    public float DamagePoints = 5.0f;

    public AudioClip gunshot;
    public AudioClip deathSound;

    public AudioSource audioSource;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if(_agent.enabled) 
        {
            float distance = Vector3.Distance(Player.transform.position, this.transform.position);
            bool shoot = false;
            bool follow = (distance < FollowDistance);

            if (follow)
            {
                float random = Random.Range(0.0f, 1.0f);
                if(random > (1.0f - AttackProbability) && distance < AttackDistance) 
                {
                    shoot = true;
                }
            }

            if (follow)
            {
                _agent.SetDestination(Player.transform.position);
            }

            if (!follow || shoot)
            {
                _agent.SetDestination(Player.transform.position);
            }

            _animator.SetBool("Shoot", shoot);
            _animator.SetBool("Run", follow);
        }
    }

    public void ShootEvent()
    {

        if (audioSource != null)
        {
            audioSource.PlayOneShot(gunshot);
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if(hit.transform.tag == "Player")
            {
                audioSource.PlayOneShot(deathSound);
                death.SetActive(true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public override void Die()
    {
        if (!enabled) return;

        if(audioSource != null)
        {
            audioSource.pitch = Time.timeScale;
            audioSource.PlayOneShot(deathSound);
        }

        _agent.enabled = false;
        _animator.SetBool("IsFollow", false);
        _animator.SetBool("Attack", false);

        _animator.SetTrigger("Death");
        Destroy(gameObject);
    }
}
