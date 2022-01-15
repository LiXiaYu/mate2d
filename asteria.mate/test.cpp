auto foo = nullptr ;
// `foo` refers to a "variable" holding `null`.

const auto inc = 42;
// `inc` refers to an "immutable variable" holding an `integer` of `42`.

auto bar = [&]() { return std::unique_ptr(inc) ;  };      // return by reference
// `bar` refers to an "immutable variable" holding a function.
// `bar()` refers to the same "variable" as `inc`.

template<typename T>
auto add(T x ) { return x + inc;  };   // return by value
// `add` refers to an "immutable variable" holding a function.
// `add(5)` refers to a "temporary" holding an `integer` of `47`.


auto add(x1,x2,x3)
{
    return x1+x2+x3;
};