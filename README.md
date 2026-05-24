# Unity Unity-health-dmg

A lightweight, flexible, and reusable Health, Armor, and Regeneration System for Unity (2D & 3D). This script uses **UnityEvents**, making it easy to connect damage, blocking, and death logic to your UI, VFX, and SFX directly in the Inspector.

---

## ✨ Features

- **Plug & Play:** Works for players, enemies, and breakable objects.
- **Armor / Defense:** Built-in flat damage reduction. Easily create tanky enemies or let players equip armor.
- **Auto-Regeneration:** Optional built-in health regeneration. Configurable delay, tick rate, and heal amounts allow you to easily create regenerating shields or slow out-of-combat healing.
- **Unity Events:** Trigger animations, particles, or UI bars without extra coding. Includes a specialized "onDamageBlocked" event for when armor absorbs a hit.
- **Encapsulated Logic:** Clean properties for "IsDead" and "CurrentHealth".
- **Flexible:** Works with any damage source (spikes, bullets, etc...).

---

## 🎮 How to Use 

1. Attach the "Health.cs" script to any GameObject.
2. Set your **Max Health**, **Defense**, and **Regeneration Settings** in the Inspector.
3. Use the **Events** section to link actions:
   - *Example:* Drag a ParticleSystem into "onDamageTaken" to play a blood/spark effect.
   - *Example:* Drag a metal clank AudioSource into "onDamageBlocked" to play a sound when armor absorbs the hit.
   - *Example:* Drag your HealthBar UI into "onHealthChanged" to update the slider.
4. To deal damage from another script, simply call:

"""
target.GetComponent<Health>().TakeDamage(15f);
"""

---

## 🧠 Design Notes

- **Unity Events over Hardcoding:** By using "UnityEvent", this script follows a decoupled design. The Health script doesn't need to know if you have a UI bar or a sound manager; it simply broadcasts that something happened, and other objects listen for it.
- **Normalized Health Value:** The "onHealthChanged" event passes a float between 0 and 1. This is intentional so you can plug it directly into a UI Slider's value or a Shader's progress property without extra math.
- **Armor Math:** The defense stat uses flat reduction ("Damage - Defense"). If an attack deals 10 damage and defense is 12, the player takes 0 damage and the "onDamageBlocked" event fires. 
- **Performance:** No "Update()" loop is used. The regeneration system relies entirely on Coroutines ("IEnumerator"). The timer goes to sleep completely when at max health, making the system extremely mobile-friendly and performant for games with hundreds of enemies.

---

## 🚀 Possible Extensions

If you want to take this system further, here are a few ideas for features you could add:

1.  **Damage Types:** Modify "TakeDamage" to accept a "DamageType" enum (e.g., Fire, Ice, Physical) to allow for elemental resistances.
2.  **Invincibility Frames (I-Frames):** Add a boolean flag and a brief Coroutine timer that prevents the object from taking consecutive instances of damage within milliseconds of each other.
3.  **Percentage Defense:** Instead of flat damage reduction, modify the math to reduce incoming damage by a percentage based on the armor value.

---

## 🛠 Unity Version

Tested in Unity6+ (should work without any problems in newer versions)

---

## 📜 License

MIT
