import { useState } from "react";
import TrueFocus from "../components/TrueFocus";
import RegisterForm from "../components/RegisterForm";
import LoginForm from "../components/LoginForm";

function Register() {
  const [activeCard, setActiveCard] = useState<0 | 1>(0);

  return (
    <div className="flex flex-col justify-center text-white gap-8 p-5">
      <div>
        <TrueFocus
          sentence="Register Login"
          manualMode={true}
          blurAmount={5}
          borderColor="blue"
          animationDuration={1}
          pauseBetweenAnimations={3}
          activeIndex={activeCard}
          onWordClick={(index) => setActiveCard(index as 0 | 1)}
        />
      </div>

      <div className="flex flex-row justify-center items-center gap-4">
        {/* Register Card */}
        <div
          className={`w-[650px] h-[750px] transition-all duration-300 rounded-xl shadow-lg ${
            activeCard === 0
              ? "opacity-100 scale-100"
              : "opacity-50 scale-95 blur-sm"
          }`}
        >
          <fieldset disabled={activeCard !== 0} className="h-full">
            <RegisterForm onSuccess={() => setActiveCard(1)} />
          </fieldset>
        </div>

        {/* Login Card */}
        <div
          className={`w-[650px] h-[750px] transition-all duration-300 rounded-xl shadow-lg ${
            activeCard === 1
              ? "opacity-100 scale-100"
              : "opacity-50 scale-95 blur-sm"
          }`}
        >
          <fieldset disabled={activeCard !== 1} className="h-full">
            <LoginForm />
          </fieldset>
        </div>
      </div>
    </div>
  );
}

export default Register;
