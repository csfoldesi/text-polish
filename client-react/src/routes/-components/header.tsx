import { Button } from "@/components/ui/button";
import { SignedIn, SignedOut, SignInButton, UserButton } from "@clerk/clerk-react";

export const Header = () => {
  return (
    <header className="flex items-center justify-between py-4 px-8 bg-primary">
      <div className="flex items-center gap-4">
        <img src="/logo.svg" alt="logo" className="h-8" />
        <h1 className="text-xl font-bold text-white">TextPolish</h1>
      </div>
      <div className="flex items-center gap-4">
        <a href="#" className="text-sm font-medium text-muted-foreground">
          Docs
        </a>
        <a href="#" className="text-sm font-medium text-muted-foreground">
          Pricing
        </a>
        <a href="#" className="text-sm font-medium text-muted-foreground">
          Company
        </a>
        <SignedOut>
          <SignInButton>
            <Button>Sign in</Button>
          </SignInButton>
        </SignedOut>
        <SignedIn>
          <UserButton />
        </SignedIn>
      </div>
    </header>
  );
};
