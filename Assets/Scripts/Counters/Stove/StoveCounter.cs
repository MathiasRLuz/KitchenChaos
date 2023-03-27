using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter
{
    public event EventHandler<OnStoveOnEventArgs> OnStoveOn;
    public event EventHandler<OnCookingEventArgs> OnCooking;

    public class OnCookingEventArgs : EventArgs {      
        public bool cooking;
    }

    public class OnStoveOnEventArgs : EventArgs {
        public bool stoveOn;
    }

    public enum State {
        Idle,
        Cooking,
        Burning,
        Burned
    }

    [SerializeField] private CookingRecipeSO[] cookingRecipeSos;
    private bool isStoveOn;
    private bool isCooking;
    [SerializeField] private State state;
    private float burningTimeMax;
    private float burningTime;
    private CookingRecipeSO cookingRecipeSO;
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // Vazio 
            if (player.HasKitchenObject()) {
                // o player está carregando algo, colocar no fogão se tiver receita que use como entrada
                CookingRecipeSO cookingRecipeSO = GetCookingRecipeSO(player.GetKitchenObject());
                if (cookingRecipeSO) {
                    this.cookingRecipeSO = cookingRecipeSO;
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    InvokeCookingEvent();
                }               
            }
        } else {
            // Ocupado
            if (!player.HasKitchenObject()) {
                // o player não está carregando algo, dar objeto pro player
                GetKitchenObject().SetKitchenObjectParent(player);
                InvokeCookingEvent();
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (state != State.Burned) HandleStove(); else if (!HasKitchenObject())  state = State.Idle;
    }

    private void HandleStove() {
        // Ligar/Desligar fogão
        isStoveOn = !isStoveOn;
        OnStoveOn?.Invoke(this, new OnStoveOnEventArgs {
            stoveOn = isStoveOn
        });
        InvokeCookingEvent();
    }

    private void Update() {
        UpdateCookingState();
    }

    private void UpdateCookingState() {
        switch (state) {
            case State.Idle:                
                if (isCooking) {
                    if (cookingRecipeSO != null) {                        
                        state = State.Cooking;
                    } else {
                        state = State.Burning;
                    }
                } 
                break;
            case State.Cooking:
                if (cookingRecipeSO == null) {
                    state = State.Burning;
                    break;
                }                
                if (!isCooking) {
                    state = State.Idle;
                    break;
                }
                GetKitchenObject().IncreaseCookingTime(Time.deltaTime);
                if (GetKitchenObject().GetCookingTime() >= cookingRecipeSO.cookingTimerMax) {                    
                    // Atualizar o objeto
                    Cook(cookingRecipeSO.output);
                    // Checar se tem receita que usa o novo objeto como entrada
                    cookingRecipeSO = GetCookingRecipeSO(GetKitchenObject());                    
                }
                break;
            case State.Burning:
                burningTime += Time.deltaTime;
                if (!isCooking) {
                    state = State.Idle;
                }
                if (burningTime >= burningTimeMax) {
                    burningTime = 0f;
                    OnCooking?.Invoke(this, new OnCookingEventArgs {
                        cooking = false
                    });
                    isStoveOn = false;
                    OnStoveOn?.Invoke(this, new OnStoveOnEventArgs {
                        stoveOn = isStoveOn
                    });
                    state = State.Burned;
                }
                break;
            case State.Burned:
                // TODO Implementar um extintor para apagar o fogo e um método para arrumar o fogão queimado                                
                break;
        }
    }

    private void InvokeCookingEvent() {
        isCooking = isStoveOn && HasKitchenObject() && state != State.Burned;
        OnCooking?.Invoke(this, new OnCookingEventArgs {
            cooking = isCooking
        });
    }

    private void Start() {
        isStoveOn = false;
        state = State.Idle;
        burningTime = 0f;
        burningTimeMax = 3f;
    }

    private CookingRecipeSO GetCookingRecipeSO(KitchenObject kitchenObject) {
        foreach (CookingRecipeSO cookingRecipeSO in cookingRecipeSos) {
            if (kitchenObject.GetKitchenObjectSO() == cookingRecipeSO.input) {
                return cookingRecipeSO;
            }
        }
        return null;
    }

    private void Cook(KitchenObjectSO kitchenObjectSO) {
        // Destrói a entrada
        GetKitchenObject().DestroySelf();
        // Spawna o saída
        KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
    }
}
