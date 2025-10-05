import { useForm } from "react-hook-form";
import SpotlightCard from "./SpotlightCard";
import { Button } from "./ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "./ui/form";
import { Input } from "./ui/input";
import { zodResolver } from "@hookform/resolvers/zod";
import z from "zod";
import { register } from "../services/AuthService";
import { useNavigate } from "react-router-dom";
import { toast } from "sonner";

interface RegisterFormProps {
  onSuccess?: () => void;
}

const formSchema = z.object({
  firstname: z
    .string()
    .min(2, { message: "First name must be at least 2 characters." })
    .max(50, { message: "First name must be at most 50 characters." }),
  lastname: z
    .string()
    .min(2, { message: "Last name must be at least 2 characters." })
    .max(50, { message: "Last name must be at most 50 characters." }),
  username: z
    .string()
    .min(2, { message: "Username must be at least 2 characters." })
    .max(50, { message: "Username must be at most 50 characters." }),
  email: z.string().email({ message: "Please enter a valid email address." }),
  password: z
    .string()
    .min(6, { message: "Password must be at least 6 characters." }),
});

function RegisterForm({ onSuccess }: RegisterFormProps) {
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      firstname: "",
      lastname: "",
      username: "",
      email: "",
      password: "",
    },
  });

  async function onSubmit(values: z.infer<typeof formSchema>) {
    try {
      const data = {
        firstname: values.firstname,
        lastname: values.lastname,
        username: values.username,
        email: values.email,
        password: values.password,
      };

      const response = await register(data);

      if (response.isSuccessful) {
        toast.success("Registration successful! You can login!");
        onSuccess?.();
        console.log("Response:", response);
      } else {
        toast.error(response.message || "Registration failed");
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
            name="firstname"
            render={({ field }) => (
              <FormItem>
                <FormLabel>FirstName</FormLabel>
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
            name="lastname"
            render={({ field }) => (
              <FormItem>
                <FormLabel>LastName</FormLabel>
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
            name="username"
            render={({ field }) => (
              <FormItem>
                <FormLabel>UserName</FormLabel>
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
            name="email"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Email</FormLabel>
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
                  <Input
                    className="bg-white border-none text-black font-bold z-10"
                    {...field}
                  />
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
            Register
          </Button>
        </form>
      </Form>
    </SpotlightCard>
  );
}
export default RegisterForm;
