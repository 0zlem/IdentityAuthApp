import SpotlightCard from "./SpotlightCard";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "./ui/form";
import { Input } from "./ui/input";
import { Button } from "./ui/button";
import z from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { login } from "../services/AuthService";
import { useNavigate } from "react-router-dom";
import { toast } from "sonner";
import { useState } from "react";
import { LiaEyeSlashSolid } from "react-icons/lia";
import { LiaEyeSolid } from "react-icons/lia";

const formSchema = z.object({
  usernameOrEmail: z
    .string()
    .min(2, { message: "Username or Email must be at least 2 characters." })
    .max(50, { message: "Username or Email must be at most 50 characters." })
    .refine(
      (val) => {
        if (val.includes("@")) {
          return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(val);
        }
        return true;
      },
      { message: "Invalid email address." }
    ),
  password: z
    .string()
    .min(6, { message: "Password must be at least 6 characters." }),
});

function LoginForm() {
  const [hidePassword, setHidePassword] = useState(false);
  const navigate = useNavigate();

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      usernameOrEmail: "",
      password: "",
    },
  });

  async function onSubmit(values: z.infer<typeof formSchema>) {
    try {
      const data = {
        usernameOrEmail: values.usernameOrEmail,
        password: values.password,
      };

      const response = await login(data);

      if (response.isSuccessful) {
        console.log("Login successful:", response);
        toast.success("Login successful!");
        localStorage.setItem("token", response.data.token);
        await navigate("/");
      } else {
        console.error("Login failed:", response);
        toast.error(response.message || "Login failed");
      }
    } catch (error) {
      console.error(error);
      toast.error("Server error, try again later.");
    }
  }

  return (
    <SpotlightCard className="custom-spotlight-card">
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
          <FormField
            control={form.control}
            name="usernameOrEmail"
            render={({ field }) => (
              <FormItem>
                <FormLabel>UserName or Email</FormLabel>
                <FormControl>
                  <Input
                    className="bg-white border-none text-black font-bold z-10"
                    {...field}
                  />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="password"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Password</FormLabel>
                <FormControl>
                  <div className="flex flex-row gap-2">
                    <Input
                      type={hidePassword ? "text" : "password"}
                      className="bg-white border-none text-black font-bold z-10"
                      {...field}
                    />
                    <fieldset
                      className="mt-2"
                      onClick={() => setHidePassword(!hidePassword)}
                    >
                      {hidePassword ? <LiaEyeSolid /> : <LiaEyeSlashSolid />}
                    </fieldset>
                  </div>
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <Button
            className="border-none hover:bg-[#853240] bg-[#3e2528] hover:text-white shadow-2xl p-5 text-lg w-full z-10 cursor-pointer"
            variant="outline"
            type="submit"
          >
            Login
          </Button>
        </form>
      </Form>
    </SpotlightCard>
  );
}

export default LoginForm;
