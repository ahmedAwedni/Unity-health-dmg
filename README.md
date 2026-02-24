# Unity Unity-health-dmg

A lightweight, flexible, and reusable Health System for Unity (2D & 3D). This script uses **UnityEvents**, making it easy to connect damage and death logic to your UI, VFX, and SFX directly in the Inspector.

---

## ✨ Features

- **Plug & Play:** Works for players, enemies, and breakable objects.
- **Unity Events:** Trigger animations, particles, or UI bars without extra coding.
- **Encapsulated Logic:** Clean properties for IsDead and CurrentHealth.
- **Flexible:** Works with any damage source (spikes, bullets, falling).

---

## 🎮 How to Use 

1. Attach the Health.cs script to any GameObject.
2. Set your **Max Health** in the Inspector.
3. Use the **Events** section to link actions:
   - *Example:* Drag a ParticleSystem into onDamageTaken to play a blood/spark effect.
   - *Example:* Drag your HealthBar UI into onHealthChanged to update the slider.
4. To deal damage from another script, simply call:
   target.GetComponent<Health>().TakeDamage(10f);

---

## 🧠 Design Notes

- **Unity Events over Hardcoding:** By using UnityEvent, this script follows a "decoupled" design. The Health script doesn't need to know if you have a UI bar or a sound manager; it simply "broadcasts" that something happened, and other objects listen for it.
- **Normalized Health Value:** The onHealthChanged event passes a float between 0 and 1. This is intentional so you can plug it directly into a UI Slider's value or a Shader's progress property without extra math.
- **Encapsulation:** The health variable is private (_currentHealth) to prevent other scripts from accidentally setting it to a weird value (like -500). All changes must go through the TakeDamage or Heal methods to ensure the logic stays consistent.
- **Performance:** No Update() loop is used. The script only sleeps until a method is called, making it extremely mobile-friendly and performant for games with hundreds of enemies.

---

## 🚀 Possible Extensions

If you want to take this system further, here are a few ideas for features you could :

1.  **Damage Types:** Modify TakeDamage to accept a "DamageType" enum (e.g., Fire, Ice, Physical) to allow for elemental resistances.
2.  **Armor/Defense:** Add a defense variable that reduces the incoming amount before it is subtracted from health.
3.  **Health Regeneration:** Add a simple Coroutine that restores a small amount of health every second if the object hasn't taken damage recently.

---

## 🛠 Unity Version

Tested in Unity6+ (should work without any problems in newer versions)
